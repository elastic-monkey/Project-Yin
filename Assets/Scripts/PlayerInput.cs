using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public static bool OnlyMenus = false;
	private static Dictionary<Axes, int> _keyDown = new Dictionary<Axes, int>()
	{
		{ Axes.HorizontalDpad, 0 },
		{ Axes.VerticalDpad, 0 }
	};

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

		if (axis.Equals(Axes.HorizontalDpad) || axis.Equals(Axes.VerticalDpad))
		{
			return (Mathf.Abs(_keyDown[axis]) == 1);
		}

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

	private void Update()
	{
		UpdateAxisBtnDown(Axes.HorizontalDpad);
		UpdateAxisBtnDown(Axes.VerticalDpad);
	}

	private void UpdateAxisBtnDown(Axes axis)
	{
		var currentValue = _keyDown[axis];
		var value = GetAxis(axis);

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