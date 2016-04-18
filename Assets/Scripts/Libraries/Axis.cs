
using System.Collections.Generic;

public enum Axis
{
	None,
    Horizontal,
    Vertical,
    Fire1,
    Fire2,
    Fire3,
    Ability1,
    Ability2,
    Ability3,
    Ability4,
	MouseScroll
}

public static class AxisHelper
{
    private static readonly Dictionary<Axis, string> AxisMap = new Dictionary<Axis, string>()
    {
		{ Axis.None, "NULL" },
		{ Axis.Horizontal, "Horizontal" },
        { Axis.Vertical, "Vertical" },
        { Axis.Fire1, "Fire1" },
        { Axis.Fire2, "Fire2" },
        { Axis.Fire3, "Fire3" },
        { Axis.Ability1, "Ability 1" },
        { Axis.Ability2, "Ability 2" },
        { Axis.Ability3, "Ability 3" },
        { Axis.Ability4, "Ability 4" },
		{ Axis.MouseScroll, "Mouse ScrollWheel" }
	};

    public static string EditorName(this Axis axis)
    {
        var s = string.Empty;
        AxisMap.TryGetValue(axis, out s);
        return s;
    }
}
