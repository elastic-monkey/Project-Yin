﻿using UnityEngine;
using System.Collections.Generic;

public enum Axes
{
    None,
    MovementHorizontal,
    MovementVertical,
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
}

public static class AxisHelper
{
    private static readonly Dictionary<Axes, string> _keyBindings = new Dictionary<Axes, string>()
    {
        { Axes.MovementHorizontal, "Horizontal"},
        { Axes.MovementVertical, "Vertical"},
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
        { Axes.MovementHorizontal, "X Axes"},
        { Axes.MovementVertical, "Y Axes"},
        { Axes.Confirm, "A"},
        { Axes.Back, "B"},
        { Axes.Attack, "X"},
        { Axes.Defend, "Left Bumper"},
        { Axes.Dodge, "Right Bumper"},
        { Axes.Shield, "Left"},
        { Axes.Strength, "Right"},
        { Axes.Speed, "Down"},
        { Axes.QuickInventoryChange, "Up"},
        { Axes.QuickInventoryUse, "Y"},
        { Axes.PlayerMenu, "Select"},
        { Axes.PauseMenu, "Start"}
    };

    private static readonly Dictionary<Axes, string> _keyboardKeyToScreenName = new Dictionary<Axes, string>()
    {
        { Axes.MovementHorizontal, "Arrow Keys"},
        { Axes.MovementVertical, "Arrow Keys"},
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

        if (Input.GetJoystickNames().Length > 0)
            _joystickKeyToScreenName.TryGetValue(axis, out s);
        else
            _keyboardKeyToScreenName.TryGetValue(axis, out s);

        return s;
    }

    public static string InputName(this Axes axis)
    {
        var s = string.Empty;
        if (axis == Axes.None)
            return s;
        
        _keyBindings.TryGetValue(axis, out s);

        return s;
    }
}
