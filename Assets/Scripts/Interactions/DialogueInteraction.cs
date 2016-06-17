using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueInteraction : PlayerInteraction
{
    public DialogueWindow Target;
    public DialogueType Type;
    public string DialogueID;
    private DialogueManager _myDialogues;
    private bool _feedLines;
    private bool _hasFinished;

    public bool HasFinished
    {
        get
        {
            if (_myDialogues != null)
            {
                return _hasFinished;
            }

            return false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _myDialogues = new DialogueManager();
        LoadDialogue(DialogueID, Type);
    }

    public void Update()
    {
        if (_feedLines)
        {
            if (!_myDialogues.FinishedDialogue)
            {
                if (Target.CurrentLineOver && PlayerInput.IsButtonDown(Target.ForwardKey))
                {
                    _myDialogues.GoToNext();
                    if (_myDialogues.FinishedDialogue)
                    {
                        StopInteraction();
                        _feedLines = false;
                        return;
                    }
                    Target.CurrentLine = _myDialogues.CurrentDialogueLine;
                }
            }
            else
            {
                StopInteraction();
                _feedLines = false;
            }
        }
    }

    public override bool ShouldStop()
    {
        return _hasFinished;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();

        _hasFinished = false;

        Target.SetActive(true);

        Target.CurrentLine = _myDialogues.CurrentDialogueLine;

        _feedLines = true;
    }

    public override void StopInteraction()
    {
        base.StopInteraction();

        Target.SetActive(false);
        _myDialogues.FinishedDialogue = false;
        _hasFinished = true;
    }

    public void LoadDialogue(string id, DialogueType type)
    {
        _myDialogues.Dialogues = DialogueLoader.LoadNPCDialogue(id, type);
        _myDialogues.Reset();
    }
}

[System.Serializable]
public class DialogueManager
{
    public int CurrentDialog = 0;
    public int CurrentLine = 0;
    public List<Dialogue> Dialogues;
    public bool FinishedDialogue;

    public NPCLine CurrentDialogueLine
    {
        get
        {
            return Dialogues[CurrentDialog].Lines[CurrentLine];
        }
    }

    public void GoToNext()
    {
        if ((CurrentLine + 1) >= Dialogues[CurrentDialog].Lines.Count)
        {
            CurrentLine = 0;
            FinishedDialogue = true;
        }
        else
        {
            CurrentLine++;
        }
    }

    public void Reset()
    {
        CurrentDialog = 0;
        CurrentLine = 0;
        FinishedDialogue = false;
    }
}

