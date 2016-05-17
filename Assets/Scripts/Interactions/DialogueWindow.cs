using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public bool Active;
    public Text Speaker, Line;

    private IAnimatedPanel _animatedPanel;

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
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
