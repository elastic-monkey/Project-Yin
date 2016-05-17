using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionsManager : MonoBehaviour
{
    public enum Actions
    {
        OpenMenu,
        OpenDialogue,
        OpenInfoTerminal
    }

    public RectTransform InteractionPrompt;

    private IAnimatedPanel _animatedPanel;
    private PlayerInteraction _currentInteraction;
    private Text _promptTitle;

    private void Awake()
    {
        _promptTitle = InteractionPrompt.GetComponentInChildren<Text>();
        _animatedPanel = InteractionPrompt.GetComponent<IAnimatedPanel>();

        ShowPrompt(false, null);
    }

    public void ShowPrompt(bool value, PlayerInteraction interaction)
    {
        if (_currentInteraction != null && interaction != _currentInteraction)
            return;
        
        _animatedPanel.SetVisible(value);
        _promptTitle.text = value ? interaction.PromptText : string.Empty;
        _currentInteraction = value ? interaction : null;
    }

    public void StartInteraction(Actions action, PlayerInteraction interaction)
    {
        
    }
}
