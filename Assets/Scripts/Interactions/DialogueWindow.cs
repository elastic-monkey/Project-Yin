using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public bool Active;
    public Text Speaker, Line;

    private GameManager _gameManager;
    private InteractionsManager _interactionsManager;
    private IAnimatedPanel _animatedPanel;

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _interactionsManager = _gameManager.Interactions;
    }

    public void SetActive(bool value)
    {
        Active = value;
        if (_animatedPanel != null)
        {
            _animatedPanel.SetVisible(value);
        }
    }
}
