using UnityEngine;
using System.Collections;

public class SaveTerminalInteraction : Interaction {

	public RectTransform DialogueWindow;
	public SaveTerminalMenu SaveMenu;

	protected override void Awake()
	{
		base.Awake ();
		_interactionText.text = "Interact with Save Terminal";
	}

	void Update(){
		if (_player != null)
		{
			if (PlayerInput.IsButtonDown (Axis.Fire1) && !PlayerInput.GameplayBlocked)
			{
				BlockInput (true);
				//InteractionPrompt.gameObject.SetActive (false);
				SaveMenu.NavMenu.OnSetActive (true);
			}
			else if (SaveMenu.ContinueGame)
			{
				SaveMenu.ContinueGame = false;
				SaveMenu.NavMenu.OnSetActive (false);
				BlockInput (false);
				//InteractionPrompt.gameObject.SetActive (true);
			}
		}
	}
}
