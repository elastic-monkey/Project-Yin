﻿using UnityEngine;
using System.Collections;

public class SaveTerminalInteraction : Interaction {

	public RectTransform DialogueWindow;

	protected override void Awake(){
		base.Awake ();
		DialogueWindow.gameObject.SetActive(false);
		_interactionText.text = "Interact with Save Terminal";
	}

	void Update(){
		if (_player != null) {
			if (PlayerInput.IsButtonDown (Axis.Fire1)) {
				BlockInput (true);
				InteractionPrompt.gameObject.SetActive (false);
				DialogueWindow.gameObject.SetActive (true);
			}
		}
	}

	public void SaveGame(){
		SaveLoad.Save ();
		CancelGameSave ();
	}

	public void CancelGameSave(){
		DialogueWindow.gameObject.SetActive (false);
		InteractionPrompt.gameObject.SetActive (true);
		BlockInput (false);
	}
}
