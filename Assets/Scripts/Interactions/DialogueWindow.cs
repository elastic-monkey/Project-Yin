using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueWindow : MonoBehaviour
{
    public Axis ForwardKey;
    public float LetterTime = 0.02f;
    public bool WritingLine;
    public bool Active;
    public Text Speaker, Line;

    private IAnimatedPanel _animatedPanel;
    private Coroutine _runningCoroutine;
    private bool _fastForward;
    private DialogueManager _myDialogues;

    public bool HasFinished
    {
        get
        {
            if (_myDialogues != null)
            {
                return _myDialogues.FinishedDialogue;
            }

            return false;
        }
    }

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
        _myDialogues = new DialogueManager();

    }

    private void Update()
    {
        if (!Active)
            return;
        
        if (!PlayerInput.IsButtonDown(ForwardKey))
            return;

        if (_myDialogues.FinishedDialogue)
            return;

        if (WritingLine)
            _fastForward = true;
        else
            _runningCoroutine = StartCoroutine(WriteDialogLine());
    }

    private IEnumerator WriteDialogLine()
    {
        _myDialogues.FinishedDialogue = false;
        WritingLine = true;

        Line.text = "";
        Speaker.text = _myDialogues.CurrentDialogueLine.owner;

        foreach (var letter in _myDialogues.CurrentDialogueLine.text)
        {
            Line.text += letter;
            yield return new WaitForSeconds(LetterTime);

            if (_fastForward)
                break;
        }

        Line.text = _myDialogues.CurrentDialogueLine.text;

        _fastForward = false;
        WritingLine = false;
        _myDialogues.GoToNext();
    }

    private void StopCoroutineIfExists()
    {
        if (_runningCoroutine != null)
            StopCoroutine(_runningCoroutine);
    }

    public void SetActive(bool value)
    {
        Active = value;
        if (_animatedPanel != null)
            _animatedPanel.SetVisible(value);

        if (!value)
        {
            _myDialogues.Dialogues.Clear();
            _myDialogues.Dialogues = null;
        }
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