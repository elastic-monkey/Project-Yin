using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueWindow : MonoBehaviour
{
    public Text Speaker, Line;
    [Range(0, 1)]
    public float LetterTime = 0.02f;
    public bool Active, FinishedLine, FastForward;

    private IAnimatedPanel _animatedPanel;

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
    }

    public void WriteLine(NPCLine line)
    {
        FinishedLine = false;
        StartCoroutine(WriteDialogLine(line));
    }

    private IEnumerator WriteDialogLine(NPCLine line)
    {
        FinishedLine = false;
        FastForward = false;

        Line.text = "";
        Speaker.text = line.owner;

        foreach (var letter in line.text)
        {
            Line.text += letter;

            yield return new WaitForSeconds(LetterTime);

            if (FastForward)
            {
                break;
            }
        }

        Line.text = line.text;
        FastForward = false;
        FinishedLine = true;
    }

    public void SetActive(bool value)
    {
        if (_animatedPanel != null)
        {
            _animatedPanel.SetVisible(value);
        }

        StopAllCoroutines();

        Active = value;
        FinishedLine = false;
        FastForward = false;
    }
}