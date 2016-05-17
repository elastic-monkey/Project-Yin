using System.Collections.Generic;
using System.Xml;
using System.IO;

public class DialogueLoader
{

    public static XmlDocument OpenDialogueFile(string file)
    {
        string FileToOpen = "Dialogue/" + file + ".xml";
        if (!File.Exists(FileToOpen))
        {
            return null;
        }
        else
        {
            var doc = new XmlDocument();
            doc.Load(FileToOpen);
            return doc;
        }
    }

    public static List<NPCDialogue> GetNPCDialogue(string npcName)
    {
        var list = new List<NPCDialogue>();

        var dialogueFile = OpenDialogueFile("NPCDialogue");
        var npcDialogue = dialogueFile.SelectSingleNode("/Document/" + npcName);

        foreach (XmlNode xmlDialogue in npcDialogue)
        {
            list.Add(ParseDialog(xmlDialogue));
        }

        list.Sort();

        return list;
    }

    public static NPCDialogue ParseDialog(XmlNode xmlDialogue)
    {
        var npcDialog = new NPCDialogue();
        npcDialog.Id = int.Parse(xmlDialogue.Attributes[0].Value);
        npcDialog.Lines = new List<NPCLine>();

        foreach (XmlNode line in xmlDialogue)
        {
            var npcLine = new NPCLine();
            foreach (XmlAttribute attr in line.Attributes)
            {
                switch (attr.Name)
                {
                    case "id":
                        npcLine.Id = int.Parse(attr.Value);
                        break;
                    case "owner":
                        npcLine.owner = attr.Value;
                        break;
                }
            }
            npcLine.text = line.InnerText.Trim();
            npcDialog.Lines.Add(npcLine);
        }

        npcDialog.Lines.Sort();
        return npcDialog;
    }

    public static TerminalInformation GetTerminalInfo(string terminalName)
    {
        var terminalInformation = new TerminalInformation();
        terminalInformation.logs = new List<TerminalLog>();

        var dialogueFile = OpenDialogueFile("Terminals");
        var terminalLogs = dialogueFile.SelectSingleNode("/Document/" + terminalName);

        foreach (XmlNode log in terminalLogs)
        {
            terminalInformation.logs.Add(ParseTerminalLog(log));
        }

        terminalInformation.logs.Sort();
        return terminalInformation;
    }

    public static TerminalLog ParseTerminalLog(XmlNode xmlLog)
    {
        var log = new TerminalLog();

        foreach (XmlAttribute attr in xmlLog.Attributes)
        {
            switch (attr.Name)
            {
                case "id":
                    log.Id = int.Parse(attr.Value);
                    break;
                case "title":
                    log.title = attr.Value;
                    break;
            }
        }
        log.text = xmlLog.InnerText.Trim();
        return log;
    }
}
