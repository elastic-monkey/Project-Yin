using System.Collections.Generic;

public enum Tags
{
    Player,
    Enemy,
    DangerArea
}

public static class TagsHelper
{
    private readonly static Dictionary<Tags, string> _tagStrings = new Dictionary<Tags, string>()
    {
        {Tags.Player, "Player"},
        {Tags.Enemy, "Enemy"},
        {Tags.DangerArea, "Danger Area"}
    };

    public static string TagToString(this Tags tag)
    {
        var s = string.Empty;

        _tagStrings.TryGetValue(tag, out s);

        return s;
    }
}
