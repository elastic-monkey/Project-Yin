using UnityEngine;
using System.Collections.Generic;
using Utilities;

public enum Axes
{
	None,
	Horizontal,
	Vertical,
	Confirm,
	Back,
	Attack,
	Defend,
	Dodge,
	Shield,
	Strength,
	Speed,
	QuickInventoryChange,
	QuickInventoryUse,
	PlayerMenu,
	PauseMenu,
	HorizontalJoystick,
	VerticalJoystick,
	HorizontalDpad,
	VerticalDpad
}

public static class AxesHelper
{
	private static readonly Dictionary<Axes, string> _keyboardKeyBindings = new Dictionary<Axes, string>()
	{
		{ Axes.Horizontal, "Horizontal"},
		{ Axes.Vertical, "Vertical"},
		{ Axes.HorizontalJoystick, "Horizontal"},
		{ Axes.VerticalJoystick, "Vertical"},
		{ Axes.HorizontalDpad, "Horizontal"},
		{ Axes.VerticalDpad, "Vertical"},
		{ Axes.Confirm, "Confirm"},
		{ Axes.Back, "Back"},
		{ Axes.Attack, "Attack"},
		{ Axes.Defend, "Defend"},
		{ Axes.Dodge, "Dodge"},
		{ Axes.Shield, "Shield"},
		{ Axes.Strength, "Strength"},
		{ Axes.Speed, "Speed"},
		{ Axes.QuickInventoryChange, "Inventory Change"},
		{ Axes.QuickInventoryUse, "Inventory Use"},
		{ Axes.PlayerMenu, "Player Menu"},
		{ Axes.PauseMenu, "Pause Menu"}
	};

	private static readonly Dictionary<Axes, string> _joystickKeyBindings = new Dictionary<Axes, string>()
	{
		{ Axes.Horizontal, "Horizontal Joystick"},
		{ Axes.Vertical, "Vertical Joystick"},
		{ Axes.HorizontalJoystick, "Horizontal Joystick"},
		{ Axes.VerticalJoystick, "Vertical Joystick"},
		{ Axes.HorizontalDpad, "Horizontal D-pad"},
		{ Axes.VerticalDpad, "Vertical D-pad"},
		{ Axes.Confirm, "Confirm"},
		{ Axes.Back, "Back"},
		{ Axes.Attack, "Attack"},
		{ Axes.Defend, "Defend"},
		{ Axes.Dodge, "Dodge"},
		{ Axes.Shield, "Shield"},
		{ Axes.Strength, "Strength"},
		{ Axes.Speed, "Speed"},
		{ Axes.QuickInventoryChange, "Inventory Change"},
		{ Axes.QuickInventoryUse, "Inventory Use"},
		{ Axes.PlayerMenu, "Player Menu"},
		{ Axes.PauseMenu, "Pause Menu"}
	};

	private static readonly Dictionary<Axes, string> _joystickKeyToScreenName = new Dictionary<Axes, string>()
	{
		{ Axes.Horizontal, "X Axes"},
		{ Axes.Vertical, "Y Axes"},
		{ Axes.HorizontalJoystick, "X Axes"},
		{ Axes.VerticalJoystick, "Y Axes"},
		{ Axes.HorizontalDpad, "D-pad Horizontal"},
		{ Axes.VerticalDpad, "D-pad Vertical"},
		{ Axes.Confirm, "A"},
		{ Axes.Back, "B"},
		{ Axes.Attack, "X"},
		{ Axes.Defend, "Left Bumper"},
		{ Axes.Dodge, "Right Bumper"},
		{ Axes.Shield, "D-pad Left"},
		{ Axes.Strength, "D-pad Right"},
		{ Axes.Speed, "D-pad Down"},
		{ Axes.QuickInventoryChange, "D-pad Up"},
		{ Axes.QuickInventoryUse, "Y"},
		{ Axes.PlayerMenu, "Select"},
		{ Axes.PauseMenu, "Start"}
	};

	private static readonly Dictionary<Axes, string> _keyboardKeyToScreenName = new Dictionary<Axes, string>()
	{
		{ Axes.Horizontal, "Arrow Keys"},
		{ Axes.Vertical, "Arrow Keys"},
		{ Axes.HorizontalJoystick, "Arrow Keys"},
		{ Axes.VerticalJoystick, "Arrow Keys"},
		{ Axes.HorizontalDpad, "Arrow Keys"},
		{ Axes.VerticalDpad, "Arrow Keys"},
		{ Axes.Confirm, "E"},
		{ Axes.Back, "Esc"},
		{ Axes.Attack, "Left Click"},
		{ Axes.Defend, "Space"},
		{ Axes.Dodge, "Shift"},
		{ Axes.Shield, "2"},
		{ Axes.Strength, "3"},
		{ Axes.Speed, "4"},
		{ Axes.QuickInventoryChange, "1"},
		{ Axes.QuickInventoryUse, "Q"},
		{ Axes.PlayerMenu, ""},
		{ Axes.PauseMenu, "Start"}
	};

	public static string ScreenName(this Axes axis)
	{
		var s = string.Empty;
		if (axis == Axes.None)
			return s;

		if (InputHelper.IsJoystickConnected())
		{
			_joystickKeyToScreenName.TryGetValue(axis, out s);
		}
		else
		{
			_keyboardKeyToScreenName.TryGetValue(axis, out s);
		}

		return s;
	}

	public static string InputName(this Axes axis)
	{
		var s = string.Empty;
		if (axis == Axes.None)
			return s;

		if (InputHelper.IsJoystickConnected())
		{
			_joystickKeyBindings.TryGetValue(axis, out s);
			return s;
		}
		else
		{
			_keyboardKeyBindings.TryGetValue(axis, out s);
			return s;
		}
	}

	public static Axes[] ToArray()
	{
		return (Axes[])System.Enum.GetValues(typeof(Axes));
	}
}
