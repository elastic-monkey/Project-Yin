using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float Speed = 1.0f;
    public int RotationXAxis;
    public int RotationYAxis;
    public int RotationZAxis;

    void Update()
    {

        transform.Rotate(new Vector3(RotationXAxis, RotationYAxis, RotationZAxis) * Time.deltaTime * Speed);

    }
}
