public enum Tags
{
    Player,
    Enemy
}

public static class TagHelper
{
    public static int ToHash(this Tags tag)
    {
        return tag.ToString().GetHashCode();
    }
}
