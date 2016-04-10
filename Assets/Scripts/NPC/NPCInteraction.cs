using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NPCInteraction : MonoBehaviour {

	public RectTransform InteractionPrompt;
	public RectTransform DialogueWindow;
	public float LetterTime = 0.02f;
	public float TurnSmoothing = 1f;

	private Text _interactionText;
	private Text _dialogueText;
	private Text _speakerName;
	private List<NPCDialogue> _npcDialogueList;
	private int _currentDialogue;
	private int _currentLine;
	private bool _canPress;
	private Collider _player;
	private string _npcName;

	private bool _endCurrentDialogue;
	private bool _playerInRange;

	void Awake(){
		InteractionPrompt.gameObject.SetActive (false);
		DialogueWindow.gameObject.SetActive (false);
		string parentName = transform.parent.name.ToString ();
		_npcName = parentName.Substring (4, parentName.Length-4);

		_interactionText = InteractionPrompt.GetComponentInChildren<Text>();
		Text[] textComponents = DialogueWindow.GetComponentsInChildren<Text> ();
		_interactionText.text = "Talk with " + _npcName;
		_speakerName = textComponents [0];
		_dialogueText = textComponents [1];
		_npcDialogueList = DialogueLoader.GetNPCDialogue (transform.parent.name.ToString ());

		_currentLine = 0;
		_currentDialogue = 0;
		_canPress = true;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.CompareTag ("Player")) {
			InteractionPrompt.gameObject.SetActive (true);
			_playerInRange = true;
		}
	}

	void OnTriggerExit(Collider collider){
		InteractionPrompt.gameObject.SetActive (false);
		_playerInRange = false;
	}

	void Update(){
		if (_playerInRange) {
			if (Input.GetButtonDown ("Fire1") && !_endCurrentDialogue) {
				InteractionPrompt.gameObject.SetActive (false);
				BlockInput (true);
				if (_canPress) {
					_canPress = false;
					DialogueWindow.gameObject.SetActive (true);
					StartCoroutine (WriteLine (_npcDialogueList [_currentDialogue].Lines [_currentLine]));
				}
			}

			if (_endCurrentDialogue && Input.GetButtonDown("Fire1")) {
				_endCurrentDialogue = false;
				DialogueWindow.gameObject.SetActive (false);
				BlockInput (false);
				InteractionPrompt.gameObject.SetActive (true);
			}
		}
	}

	IEnumerator WriteLine(NPCLine line){
		_dialogueText.text = "";
		_speakerName.text = line.owner;

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

	void BlockInput(bool block){
		//PlayerController.CanMove = !block;
		//PlayerAttack.CanAttack = !block;
	}
}
