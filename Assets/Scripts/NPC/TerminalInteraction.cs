using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TerminalInteraction : MonoBehaviour {

	public RectTransform InteractionPrompt;
	//public RectTransform DialogueWindow;
	public ShowHidePanel DialogueWindow;

	private Text _interactionText;
	private Text _dialogueText;
	private Text _title;

	private TerminalInformation _information;
	private Collider _player;

	private bool _playerInRange;
	private bool _over;


	void Awake(){
		InteractionPrompt.gameObject.SetActive (false);
		DialogueWindow.gameObject.SetActive (false);
		string parentName = transform.parent.name.ToString ();
		string terminalName = parentName.Substring (9, parentName.Length-9);

		_interactionText = InteractionPrompt.GetComponentInChildren<Text>();
		Text[] textComponents = DialogueWindow.GetComponentsInChildren<Text> ();
		_interactionText.text = "Interact with " + terminalName;
		_title = textComponents [0];
		_dialogueText = textComponents [1];

		_information = DialogueLoader.GetTerminalInfo (transform.parent.name.ToString ());
		_over = false;
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
			if (Input.GetButtonDown ("Fire1") && !_over) {
				InteractionPrompt.gameObject.SetActive (false);
				BlockInput (true);
				DialogueWindow.gameObject.SetActive (true);
				DialogueWindow.Visible = true;
				_title.text = _information.logs [0].title;
				_dialogueText.text = _information.logs [0].text;
				_over = true;
			} else if (Input.GetButtonDown("Fire1") && _over) {
				DialogueWindow.Visible = false;
				DialogueWindow.gameObject.SetActive (false);
				BlockInput (false);
				InteractionPrompt.gameObject.SetActive (true);
				_over = false;
			}
		}
	}

	void BlockInput(bool block){
		//PlayerController.CanMove = !block;
		//PlayerAttack.CanAttack = !block;
	}
}
