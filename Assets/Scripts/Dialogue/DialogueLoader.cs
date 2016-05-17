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

    public static List<Dialogue> LoadNPCDialogue(string npcName, DialogueType type)
    {
        var list = new List<Dialogue>();

        var dialogueFile = OpenDialogueFile(type.Filename());
        var dialogueNode = dialogueFile.SelectSingleNode("/Document/" + npcName);

        foreach (XmlNode xmlDialogue in dialogueNode)
        {
            list.Add(ParseDialogue(xmlDialogue));
        }

        list.Sort();
        return list;
    }

    public static Dialogue ParseDialogue(XmlNode xmlDialogue)
    {
        var npcDialogue = new Dialogue();
        npcDialogue.Id = xmlDialogue.Attributes.Count > 0 ? int.Parse(xmlDialogue.Attributes[0].Value) : 0;
        npcDialogue.Lines = new List<NPCLine>();

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

                    case "title":
                        npcLine.owner = attr.Value;
                        break;
                }
            }
            npcLine.text = line.InnerText.Trim();
            npcDialogue.Lines.Add(npcLine);
        }

        npcDialogue.Lines.Sort();
        return npcDialogue;
    }
}

public enum DialogueType
{
    NPC,
    Terminal
}

public static class DialogueHelper
{
    public static string Filename(this DialogueType type)
    {
        switch (type)
        {
            case DialogueType.NPC:
                return "NPCDialogue";

            case DialogueType.Terminal:
                return "Terminals";
        }

        return string.Empty;
    }
}