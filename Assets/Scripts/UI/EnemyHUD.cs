using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Slider HealthSlider;

    private Health _health;
	private Quaternion _lookAtScreen;

	void Awake()
	{
		var targetCam = FindObjectOfType<IsometricCamera>();
		_lookAtScreen = Quaternion.Euler(targetCam.Angle, 0, 0);

        _health = GetComponentInParent<Health>();

        HealthSlider.minValue = 0;
        HealthSlider.maxValue = _health.MaxHealth;
        HealthSlider.value = _health.CurrentHealth;
	}

	void Update()
	{
		transform.rotation = _lookAtScreen;
        HealthSlider.maxValue = _health.MaxHealth;
        HealthSlider.value = _health.CurrentHealth;
	}
}
