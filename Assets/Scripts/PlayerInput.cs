using UnityEngine;

public abstract class PlayerInput
{
    public static bool OnlyMenus = false;

    public static float GetAxis(Axis axis)
    {
        if (InvalidAxis(axis))
            return 0;
        
        return Input.GetAxis(axis.EditorName());
    }

    public static float GetAxisRaw(Axis axis)
    {
        if (InvalidAxis(axis))
            return 0;
        
        return Input.GetAxisRaw(axis.EditorName());
    }

    public static bool IsButtonDown(Axis axis)
    {
        if (InvalidAxis(axis))
            return false;

        return Input.GetButtonDown(axis.EditorName());
    }

    public static bool IsButtonPressed(Axis axis)
    {
        if (InvalidAxis(axis))
            return false;
        
        return Input.GetButton(axis.EditorName());
    }

    public static bool IsButtonUp(Axis axis)
    {
        if (InvalidAxis(axis))
            return false;
        
        return Input.GetButtonUp(axis.EditorName());
    }

    public static bool InvalidAxis(Axis axis)
    {
        return axis == Axis.None;
    }
}