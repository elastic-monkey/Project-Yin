using System;
using System.Collections.Generic;

public class TerminalInformation
{
    public int Id;
    public List<TerminalLog> logs;
}

public class TerminalLog : IComparable<TerminalLog>
{
    public int Id;
    public string title;
    public string text;

    int IComparable<TerminalLog>.CompareTo(TerminalLog other)
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
