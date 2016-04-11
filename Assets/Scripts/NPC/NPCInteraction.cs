using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NPCInteraction : Interaction {

	public RectTransform DialogueWindow;
	public float LetterTime = 0.02f;
	public float TurnSmoothing = 1f;

	private List<NPCDialogue> _npcDialogueList;
	private int _currentDialogue;
	private int _currentLine;
	private bool _canPress;
	private string _npcName;

	private bool _endCurrentDialogue;

	protected override void Awake(){
		base.Awake ();

		DialogueWindow.gameObject.SetActive (false);
		string parentName = transform.parent.name.ToString ();
		_npcName = parentName.Substring (4, parentName.Length-4);

		Text[] textComponents = DialogueWindow.GetComponentsInChildren<Text> ();
		_interactionText.text = "Talk with " + _npcName;
		_title = textComponents [0];
		_dialogueText = textComponents [1];
		_npcDialogueList = DialogueLoader.GetNPCDialogue (transform.parent.name.ToString ());

		_currentLine = 0;
		_currentDialogue = 0;
		_canPress = true;
	}

	void Update(){
		if (_player != null) {
			if (_player.Input.Fire1Down && !_endCurrentDialogue) {
				InteractionPrompt.gameObject.SetActive (false);
				BlockInput (true);
				if (_canPress) {
					_canPress = false;
					DialogueWindow.gameObject.SetActive (true);
					StartCoroutine (WriteLine (_npcDialogueList [_currentDialogue].Lines [_currentLine]));
				}
			}

			if (_player.Input.Fire1Down && _endCurrentDialogue) {
				_endCurrentDialogue = false;
				DialogueWindow.gameObject.SetActive (false);
				BlockInput (false);
				InteractionPrompt.gameObject.SetActive (true);
			}
		}
	}

	IEnumerator WriteLine(NPCLine line){
		_dialogueText.text = "";
		_title.text = line.owner;

		foreach (char letter in line.text.ToCharArray()) {
			_dialogueText.text += letter;
			yield return new WaitForSeconds (LetterTime);
		}

		if (_currentLine < _npcDialogueList [_currentDialogue].Lines.Count-1) {
			_currentLine++;
		} else {
			_currentLine = 0;
			_endCurrentDialogue = true;
			if (_currentDialogue < _npcDialogueList.Count-1) {
				_currentDialogue++;
			} else {
				_currentDialogue = 0;
			}
		}

		_canPress = true;
	}
}
