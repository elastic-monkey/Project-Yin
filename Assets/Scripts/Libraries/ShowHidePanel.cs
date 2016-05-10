using UnityEngine;
using System.Collections;

public class ShowHidePanel : MonoBehaviour, IAnimatedPanel
{
	public Utils.WindowPositions HidePosition, ShowPosition;
	public RectTransform Container;
	public float AnimationSpeed;

    [SerializeField]
    private bool _visible = false;
    private Coroutine _oldAnim = null;

    public void SetVisible(bool value)
    {
        _visible = value;
        if (_oldAnim != null)
            StopCoroutine(_oldAnim);
        
        _oldAnim = StartCoroutine(AnimateWindow());
    }

	private IEnumerator AnimateWindow()
	{
		Vector2 targetMin, targetMax;

        if (_visible)
		{
			Utils.GetAnchorsForWindowPosition(ShowPosition, out targetMin, out targetMax);
			while (true)
			{
				if (Utils.LerpRectTransformAnchors(Container, targetMin, targetMax, Time.unscaledDeltaTime * AnimationSpeed))
					yield break;

				yield return null;
			}
		}
		else
		{
			Utils.GetAnchorsForWindowPosition(HidePosition, out targetMin, out targetMax);
			while (true)
			{
				if (Utils.LerpRectTransformAnchors(Container, targetMin, targetMax, Time.unscaledDeltaTime * AnimationSpeed))
					yield break;

				yield return null;
			}
		}
	}
}
