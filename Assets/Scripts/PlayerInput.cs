using UnityEngine;

public abstract class PlayerInput
{
    public static bool OnlyMenus = false;
    public static bool OnlyOpenMenu = false;

    public static float GetAxis(Axes axis)
    {
        if (InvalidAxis(axis))
            return 0;
        
        return Input.GetAxis(axis.InputName());
    }

    public static float GetAxisRaw(Axes axis)
    {
        if (InvalidAxis(axis))
            return 0;
        
        return Input.GetAxisRaw(axis.InputName());
    }

    public static bool IsButtonDown(Axes axis)
    {
        if (InvalidAxis(axis))
            return false;

        return Input.GetButtonDown(axis.InputName());
    }

    public static bool IsButtonPressed(Axes axis)
    {
        if (InvalidAxis(axis))
            return false;
        
        return Input.GetButton(axis.InputName());
    }

    public static bool IsButtonUp(Axes axis)
    {
        if (InvalidAxis(axis))
            return false;
        
        return Input.GetButtonUp(axis.InputName());
    }

    public static bool InvalidAxis(Axes axis)
    {
        return (axis == Axes.None);
    }
}