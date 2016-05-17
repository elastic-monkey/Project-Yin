using System;
using System.Collections.Generic;

public class NPCDialogue : IComparable<NPCDialogue>
{
    public int Id;
    public List<NPCLine> Lines;

    int IComparable<NPCDialogue>.CompareTo(NPCDialogue other)
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
