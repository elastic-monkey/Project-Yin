﻿using UnityEngine;

public abstract class PlayerInput
{
    public static bool GameplayBlocked = false;

    public static float GetAxis(Axis axis)
    {
        return Input.GetAxis(axis.EditorName());
    }

    public static float GetAxisRaw(Axis axis)
    {
        return Input.GetAxisRaw(axis.EditorName());
    }

    public static bool IsButtonDown(Axis axis)
    {
        return Input.GetButtonDown(axis.EditorName());
    }

    public static bool IsButtonPressed(Axis axis)
    {
        return Input.GetButton(axis.EditorName());
    }

    public static bool IsButtonUp(Axis axis)
    {
        return Input.GetButtonUp(axis.EditorName());
    }
}