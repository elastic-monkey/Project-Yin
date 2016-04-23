using UnityEngine;

public class Rotator : MonoBehaviour {

    public int RotationXAxis;
    public int RotationYAxis;
    public int RotationZAxis;

	void Update () {

        transform.Rotate(new Vector3(RotationXAxis, RotationYAxis, RotationZAxis) * Time.deltaTime);

	}
}
