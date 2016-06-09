using UnityEngine;
using System.Collections;
using Utilities;

public class InputTester : MonoBehaviour
{
    private void Update()
    {
        foreach (var axis in AxesHelper.ToArray())
            DebugAxis(axis); 
    }

    private void DebugAxis(Axes axis)
    {
        var value = PlayerInput.IsButtonDown(axis);
        if (value)
            Debug.Log(axis.InputName() + " --> " + value);
    }
}
