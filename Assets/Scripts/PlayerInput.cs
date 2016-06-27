using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerInput : MonoBehaviour
{
	public static bool OnlyMenus = false;
	private static Dictionary<Axes, int> _keyDown = new Dictionary<Axes, int>()
	{
		{ Axes.MenusHorizontal, 0 },
		{ Axes.MenusVertical, 0 }
	};

	public static float GetAxis(Axes axis)
	{
		if (InvalidAxis(axis))
			return 0;

        var kbdValue = Input.GetAxis(axis.GetKeyboardInputName());
        var jstValue = Input.GetAxis(axis.GetJoystickInputName());

        return Mathf.Clamp(kbdValue + jstValue, -1, 1);
	}

	public static float GetAxisRaw(Axes axis)
	{
		if (InvalidAxis(axis))
			return 0;

        var kbdValue = Input.GetAxisRaw(axis.GetKeyboardInputName());
        var jstValue = Input.GetAxisRaw(axis.GetJoystickInputName());

        return Mathf.Clamp(kbdValue + jstValue, -1, 1);
	}

	public static bool IsButtonDown(Axes axis)
	{
		if (InvalidAxis(axis))
			return false;

        if (axis == Axes.MenusHorizontal)
        {
            return (Mathf.Abs(_keyDown[Axes.MenusHorizontal]) == 1 || Input.GetButtonDown(Axes.MenusHorizontal.GetKeyboardInputName()));
        }
        else if (axis == Axes.MenusVertical)
        {
            return (Mathf.Abs(_keyDown[Axes.MenusVertical]) == 1 || Input.GetButtonDown(Axes.MenusVertical.GetKeyboardInputName()));
        }
        else if (axis == Axes.QuickInventoryChange)
        {
            return (_keyDown[Axes.MenusVertical] == 1 || Input.GetButtonDown(Axes.QuickInventoryChange.GetKeyboardInputName()));
        }
        else if (axis == Axes.Speed)
        {
            return (_keyDown[Axes.MenusVertical] == -1 || Input.GetButtonDown(Axes.Speed.GetKeyboardInputName()));
        }
        else if (axis.Equals(Axes.Shield))
        {
            return (_keyDown[Axes.MenusHorizontal] == -1 || Input.GetButtonDown(Axes.Shield.GetKeyboardInputName()));
        }
        else if (axis == Axes.Strength)
        {
            return (_keyDown[Axes.MenusHorizontal] == 1 || Input.GetButtonDown(Axes.Strength.GetKeyboardInputName()));
        }

        var kbdValue = Input.GetButtonDown(axis.GetKeyboardInputName());
        var jstValue = Input.GetButtonDown(axis.GetJoystickInputName());

        return (kbdValue || jstValue);
	}

	public static bool IsButtonPressed(Axes axis)
	{
		if (InvalidAxis(axis))
			return false;

        var kbdValue = Input.GetButton(axis.GetKeyboardInputName());
        var jstValue = Input.GetButton(axis.GetJoystickInputName());

        return (kbdValue || jstValue);
	}

	public static bool IsButtonUp(Axes axis)
	{
		if (InvalidAxis(axis))
			return false;

        var kbdValue = Input.GetButtonUp(axis.GetKeyboardInputName());
        var jstValue = Input.GetButtonUp(axis.GetJoystickInputName());

        return (kbdValue || jstValue);
	}

	public static bool InvalidAxis(Axes axis)
	{
		return (axis == Axes.None);
	}

	private void Update()
	{
		UpdateAxisBtnDown(Axes.MenusHorizontal);
		UpdateAxisBtnDown(Axes.MenusVertical);
	}

	private void UpdateAxisBtnDown(Axes axis)
	{
		var currentValue = _keyDown[axis];
        var value = Input.GetAxis(axis.GetJoystickInputName());

		if (value == 0f)
		{
			_keyDown[axis] = 0;
		}
		else if (value > 0f)
		{
			if (currentValue <= 0)
			{
				_keyDown[axis] = 1;
			}
			else
			{
				_keyDown[axis] = 2;
			}
		}
		else if (value < 0f)
		{
			if (currentValue >= 0)
			{
				_keyDown[axis] = -1;
			}
			else
			{
				_keyDown[axis] = -2;
			}
		}
	}
}