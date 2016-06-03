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
    public NPCLine CurrentLine;

    private IAnimatedPanel _animatedPanel;
    private Coroutine _runningCoroutine;
    private bool _fastForward;
    public bool CurrentLineOver;
    private bool _hasFinished;
   
    public bool HasFinished
    {
        get
        {
            if (CurrentLine != null)
            {
                return _hasFinished;
            }

            return false;
        }
    }

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
    }

    private void Update()
    {
        if (!Active)
            return;
        
        if (CurrentLine == null)
        {
            return;
        }

        if (WritingLine && PlayerInput.IsButtonDown(ForwardKey))
        {
            _fastForward = true;
        }
        else if (!WritingLine && CurrentLine != null)
        {
            _runningCoroutine = StartCoroutine(WriteDialogLine());
        }
    }

    private IEnumerator WriteDialogLine()
    {
        _hasFinished = false;
        WritingLine = true;
        CurrentLineOver = false;

        Line.text = "";
        Speaker.text = CurrentLine.owner;

        foreach (var letter in CurrentLine.text)
        {
            Line.text += letter;
            yield return new WaitForSeconds(LetterTime);

            if (_fastForward)
                break;
        }

        Line.text = CurrentLine.text;

        _fastForward = false;
        WritingLine = false;
        CurrentLineOver = true;
        CurrentLine = null;
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
            CurrentLine = null;
        }
    }

   
}