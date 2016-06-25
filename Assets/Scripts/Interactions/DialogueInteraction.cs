using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueInteraction : PlayerInteraction
{
    public Axes ForwardKey = Axes.Confirm;
    public DialogueType Type;
    public string DialogueID;

    private DialogueWindow _dialogueWindow;
    private DialogueManager _dialogue;

    public bool HasFinished
    {
        get
        {
            if (_dialogue != null)
            {
                return _dialogue.FinishedDialogue;
            }

            return true;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _dialogueWindow = GameManager.DialogueWindow;

        _dialogue = new DialogueManager();
        LoadDialogue(DialogueID, Type);
    }

    protected override void Update()
    {
        base.Update();

        if (!Ongoing)
            return;

        if (HasFinished)
        {
            StopInteraction();
            return;
        }

        if (PlayerInput.IsButtonDown(ForwardKey))
        {
            if (_dialogueWindow.FinishedLine)
            {
                _dialogue.GoToNext();
                if (HasFinished)
                {
                    StopInteraction();
                    return;
                }

                _dialogueWindow.WriteLine(_dialogue.CurrentDialogueLine);
            }
            else
            {
                _dialogueWindow.FastForward = true;
            }
        }
    }

    protected override bool ShouldStop()
    {
        return HasFinished;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();

        _dialogueWindow.SetActive(true);
        _dialogueWindow.WriteLine(_dialogue.CurrentDialogueLine);
    }

    public override void StopInteraction()
    {
        base.StopInteraction();

        _dialogueWindow.SetActive(false);
        _dialogue.Reset();
    }

    private void LoadDialogue(string id, DialogueType type)
    {
        _dialogue.Dialogues = DialogueLoader.LoadNPCDialogue(id, type);
        _dialogue.Reset();
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

