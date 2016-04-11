using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TerminalInteraction : Interaction {

	public ShowHidePanel DialogueWindow;

	private TerminalInformation _information;
	private bool _dialogueOpen;

	protected override void Awake(){
		base.Awake ();

		string parentName = transform.parent.name.ToString ();
		string terminalName = parentName.Substring (9, parentName.Length-9);

		_interactionText = InteractionPrompt.GetComponentInChildren<Text>();
		Text[] textComponents = DialogueWindow.GetComponentsInChildren<Text> ();
		_interactionText.text = "Interact with " + terminalName;
		_title = textComponents [0];
		_dialogueText = textComponents [1];

		_information = DialogueLoader.GetTerminalInfo (transform.parent.name.ToString ());
		_dialogueOpen = false;
	}

	void Update(){
		if (_player != null) {
			if (_player.Input.Fire1Down) {
				if (!_dialogueOpen) {
					BlockInput (true);
					InteractionPrompt.gameObject.SetActive (false);
					DialogueWindow.Visible = true;
					_title.text = _information.logs [0].title;
					_dialogueText.text = _information.logs [0].text;
					_dialogueOpen = true;
				} else if (_dialogueOpen) {
					DialogueWindow.Visible = false;
					InteractionPrompt.gameObject.SetActive (true);
					BlockInput (false);
					_dialogueOpen = false;
				}
			}
		}
	}
}
