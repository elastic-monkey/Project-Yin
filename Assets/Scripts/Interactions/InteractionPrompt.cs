using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public Axis InteractKey;
    public bool WaitingInput;
    public PlayerInteraction Interaction;

    private Text _myText;
    private IAnimatedPanel _animatedPanel;
    private bool _waitForInputNextFrame;

    public Text Title
    {
        get
        {
            if (_myText == null)
                _myText = GetComponentInChildren<Text>();

            return _myText;
        }
    }

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
        _animatedPanel.SetVisible(false);
    }

    private void Update()
    {
        if (Interaction == null || !WaitingInput)
            return;

        if (Interaction.Active)
            return;

        if (PlayerInput.IsButtonDown(InteractKey))
        {
            Interaction.SetActive(true);
            Show(false, Interaction);
            WaitingInput = false;
        }
    }

    private void LateUpdate()
    {
        if (_waitForInputNextFrame)
        {
            _waitForInputNextFrame = false;
            WaitingInput = true;
        }
    }

    public void Show(bool value, PlayerInteraction interaction)
    {
        if (interaction != Interaction && value)
        {
            Title.text = interaction.PromptText;
            Interaction = interaction;
            _waitForInputNextFrame = value;
            WaitingInput = false;
            _animatedPanel.SetVisible(value);
        }
        else if (interaction == Interaction && !value)
        {
            Title.text = string.Empty;
            Interaction = null;
            _waitForInputNextFrame = value;
            WaitingInput = false;
            _animatedPanel.SetVisible(value);
        }
    }
}
