using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public Axes InteractKey;
    public bool WaitingInput;

    private Text _myText;
    private IAnimatedPanel _animatedPanel;
    private PlayerInteraction _currentInteraction;
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
        if (_currentInteraction == null || !WaitingInput)
            return;

        if (!PlayerInput.IsButtonDown(InteractKey))
            return;
        
        _currentInteraction.StartInteraction();
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
        if (value)
        {
            if (interaction == _currentInteraction)
                return;
            
            Title.text = interaction.PromptText;
            _currentInteraction = interaction;
            _waitForInputNextFrame = true;
            WaitingInput = false;
            _animatedPanel.SetVisible(value);
        }
        else
        {
            if (interaction != _currentInteraction)
                return;
            
            Title.text = string.Empty;
            _currentInteraction = null;
            _waitForInputNextFrame = false;
            WaitingInput = false;
            _animatedPanel.SetVisible(value);
        }
    }
}
