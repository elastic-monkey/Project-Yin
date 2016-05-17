using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueInteraction : PlayerInteraction
{
    public DialogueWindow DialogueWindow;
    public DialogueType Type;
    public Axis CloseKey;
    public string DialogueID;
    public float LetterTime = 0.02f;
    public bool WritingLine;
    public DialogueManager MyDialogues;

    private Coroutine _runningCoroutine;
    private bool _fastForward;

    protected override void Awake()
    {
        base.Awake();

        MyDialogues.Dialogues = DialogueLoader.LoadNPCDialogue(DialogueID, Type);
        MyDialogues.Reset();
    }

    private void Update()
    {
        if (!Active)
            return;
        
        if (PlayerInput.IsButtonDown(CloseKey))
        {
            if (MyDialogues.FinishedDialogue)
            {
                _interactionPrompt.Show(true, this);
                SetActive(false);
            }
            else if (WritingLine)
            {
                _fastForward = true;
            }
            else
            {
                _runningCoroutine = StartCoroutine(WriteDialogLine());
            }
        }
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        DialogueWindow.SetActive(value);
        MyDialogues.FinishedDialogue = false;

        StopCoroutineIfExists();
        if (value)
            _runningCoroutine = StartCoroutine(WriteDialogLine());
    }

    private IEnumerator WriteDialogLine()
    {
        MyDialogues.FinishedDialogue = false;
        WritingLine = true;

        DialogueWindow.Line.text = "";
        DialogueWindow.Speaker.text = MyDialogues.CurrentDialogueLine.owner;

        foreach (var letter in MyDialogues.CurrentDialogueLine.text)
        {
            DialogueWindow.Line.text += letter;
            yield return new WaitForSeconds(LetterTime);

            if (_fastForward)
                break;
        }

        DialogueWindow.Line.text = MyDialogues.CurrentDialogueLine.text;

        _fastForward = false;
        WritingLine = false;
        MyDialogues.GoToNext();
    }

    private void StopCoroutineIfExists()
    {
        if (_runningCoroutine != null)
            StopCoroutine(_runningCoroutine);
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

            if ((CurrentDialog + 1) < Dialogues.Count)
            {
                CurrentDialog++;
            }
            else
            {
                CurrentDialog = 0;
            }
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