using UnityEngine;
using System.Collections;
using Utilities;

public class SlidePanel : MonoBehaviour, IAnimatedPanel
{
	public PanelHelper.WindowPositions HidePosition, ShowPosition;
	public RectTransform Container;
	public float AnimationSpeed;

    public bool Visible;
    private Coroutine _oldAnim = null;

    public void Start()
    {
        SetVisible(Visible);
    }

    public void SetVisible(bool value)
    {
        Visible = value;
        if (_oldAnim != null)
            StopCoroutine(_oldAnim);
        
        _oldAnim = StartCoroutine(AnimateWindow());
    }

	private IEnumerator AnimateWindow()
	{
		Vector2 targetMin, targetMax;

        if (Visible)
		{
			PanelHelper.GetAnchorsForWindowPosition(ShowPosition, out targetMin, out targetMax);
			while (true)
			{
				if (PanelHelper.LerpRectTransformAnchors(Container, targetMin, targetMax, Time.unscaledDeltaTime * AnimationSpeed))
					yield break;

				yield return null;
			}
		}
		else
		{
			PanelHelper.GetAnchorsForWindowPosition(HidePosition, out targetMin, out targetMax);
			while (true)
			{
				if (PanelHelper.LerpRectTransformAnchors(Container, targetMin, targetMax, Time.unscaledDeltaTime * AnimationSpeed))
					yield break;

				yield return null;
			}
		}
	}
}
