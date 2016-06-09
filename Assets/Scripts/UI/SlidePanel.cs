using UnityEngine;
using System.Collections;
using Utilities;

public class SlidePanel : MonoBehaviour, IAnimatedPanel
{
    public PanelHelper.WindowPositions HidePosition, ShowPosition;
    public RectTransform Container;
    public float Duration = 1f;

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
        var timePassed = 0f;
        var invDuration = 1f / Duration;
        Vector2 targetMin, targetMax;

        PanelHelper.GetAnchorsForWindowPosition(Visible ? ShowPosition : HidePosition, out targetMin, out targetMax);

        while (timePassed <= 1f)
        {
            PanelHelper.LerpRectTransformAnchors(Container, targetMin, targetMax, timePassed * invDuration);

            yield return null;

            timePassed += Time.unscaledDeltaTime;
        }
    }
}
