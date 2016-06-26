using System;
using System.Collections.Generic;

public class Dialogue : IComparable<Dialogue>
{
    public int Id;
    public List<NPCLine> Lines;

    int IComparable<Dialogue>.CompareTo(Dialogue other)
    {
        if (this.Id > other.Id)
        {
            return 1;
        }
        else if (this.Id < other.Id)
        {
            return -1;
        }
        return 0;
    }
}

[System.Serializable]
public class NPCLine : IComparable<NPCLine>
{
    public int Id;
    public string owner;
    public string text;

    int IComparable<NPCLine>.CompareTo(NPCLine other)
    {
        if (this.Id > other.Id)
        {
            return 1;
        }
        else if (this.Id < other.Id)
        {
            return -1;
        }
        return 0;
    }
}
