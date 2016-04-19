using UnityEngine;

public class EnemyHUD : MonoBehaviour
{
	private Quaternion _lookAtScreen;

	void Awake()
	{
		var targetCam = FindObjectOfType<IsometricCamera>();
		_lookAtScreen = Quaternion.Euler(targetCam.Angle, 0, 0);
	}

	void Update()
	{
		transform.rotation = _lookAtScreen;
	}
}
