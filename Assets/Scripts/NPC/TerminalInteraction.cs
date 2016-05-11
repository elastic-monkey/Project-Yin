using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TerminalInteraction : Interaction
{

	public string TerminalName;
    public ShowHidePanel DialogueWindow;

    private TerminalInformation _information;
    private bool _dialogueOpen;

    protected override void Awake()
    {
        base.Awake();

        _interactionText = InteractionPrompt.GetComponentInChildren<Text>();
        Text[] textComponents = DialogueWindow.GetComponentsInChildren<Text>();
		_interactionText.text = "Interact with " + TerminalName;
        _title = textComponents[0];
        _dialogueText = textComponents[1];

		_information = DialogueLoader.GetTerminalInfo(TerminalName);
        _dialogueOpen = false;
    }

    void Update()
    {
        if (_player != null)
        {
            if (PlayerInput.IsButtonDown(Axis.Fire1))
            {
                if (!_dialogueOpen)
                {
                    BlockInput(true);
                    InteractionPrompt.gameObject.SetActive(false);
					_title.text = _information.logs[0].title;
					_dialogueText.text = _information.logs[0].text;
					_dialogueOpen = true;
					DialogueWindow.SetVisible(true);
                }
                else if (_dialogueOpen)
                {
					Debug.Log ("Closing window");
                    DialogueWindow.SetVisible(false);
                    InteractionPrompt.gameObject.SetActive(true);
                    BlockInput(false);
                    _dialogueOpen = false;
                }
            }
        }
    }
}
