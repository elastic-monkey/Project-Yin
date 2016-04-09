using UnityEngine;
using System.Collections;

public enum Tags
{
    Player,
    Enemy,
    PlayerRange,
    EnemyRange
}

public static class TagHelper
{
    public static int ToHash(this Tags tag)
    {
        return tag.ToString().GetHashCode();
    }
}
