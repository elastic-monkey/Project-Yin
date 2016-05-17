using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NPCInteraction : PlayerInteraction
{
    public DialogueWindow DialogueWindow;
    public string FileID;
    public float LetterTime = 0.02f;

    private List<NPCDialogue> _npcDialogues;
    private Text _speakerText, _lineText;
    private int _currentDialogue;
    private int _currentLine;
    private bool _canPress;
    private bool _skipLine;
    private bool _linePlaying;
    private string _npcName;

    private bool _endCurrentDialogue;

    private void Awake()
    {
		_npcName = FileID;
        _speakerText = DialogueWindow.Speaker;
        _lineText = DialogueWindow.Line;
        _npcDialogues = DialogueLoader.GetNPCDialogue(_npcName);
        _currentLine = 0;
        _currentDialogue = 0;

        _canPress = true;
		_linePlaying = false;
		_skipLine = false;
    }   

    void Update()
    {
//        if (_player != null)
//        {
//            if (PlayerInput.IsButtonDown(Axis.Fire1) && _linePlaying)
//            {
//                _skipLine = true;
//            }
//            if (PlayerInput.IsButtonDown(Axis.Fire1) && !_endCurrentDialogue)
//            {
//                //InteractionPrompt.gameObject.SetActive(false);
//                BlockInput(true);
//                if (_canPress)
//                {
//                    _canPress = false;
//                    //DialogueWindow.gameObject.SetActive(true);
//                    StartCoroutine(WriteLine(_npcDialogueList[_currentDialogue].Lines[_currentLine]));
//                }
//            }
//            if (PlayerInput.IsButtonDown(Axis.Fire1) && _endCurrentDialogue)
//            {
//                _endCurrentDialogue = false;
//                //DialogueWindow.gameObject.SetActive (false);
//                BlockInput(false);
//                //InteractionPrompt.gameObject.SetActive (true);
//            }
//        }
    }

    IEnumerator WriteLine(NPCLine line)
    {
//        _dialogueText.text = "";
//        _title.text = line.owner;
//        _linePlaying = true;
//
//        foreach (char letter in line.text.ToCharArray())
//        {
//            if (_skipLine)
//            {
//                _skipLine = false;
//                _dialogueText.text = line.text;
//                break;
//            }
//            _dialogueText.text += letter;
//            yield return new WaitForSeconds(LetterTime);
//        }
//
//        if (_currentLine < _npcDialogueList[_currentDialogue].Lines.Count - 1)
//        {
//            _currentLine++;
//        }
//        else
//        {
//            _currentLine = 0;
//            _endCurrentDialogue = true;
//            if (_currentDialogue < _npcDialogueList.Count - 1)
//            {
//                _currentDialogue++;
//            }
//            else
//            {
//                _currentDialogue = 0;
//            }
//        }
//
//        _canPress = true;
//        _linePlaying = false;
        yield return null;
    }
}
