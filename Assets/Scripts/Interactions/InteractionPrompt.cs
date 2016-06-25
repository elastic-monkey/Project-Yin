using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public Text TitleText;
    private IAnimatedPanel _animatedPanel;

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
        _animatedPanel.SetVisible(false);
    }

    public void SetVisible(bool value, Axes key = Axes.Confirm, string title = null)
    {
        if (value)
        {
            TitleText.text = string.Concat(title, " (", key.ScreenName(), ")");
            _animatedPanel.SetVisible(value);
        }
        else
        {
            TitleText.text = string.Empty;
            _animatedPanel.SetVisible(value);
        }
    }
}
