using UnityEngine;
using System.Collections;

public class PanelZoomInOut : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    [Range(5, 60)]
    public int CheckRate = 15;
    public float AnimationSpeed = 8f;
    public bool Visible = true;

    private RectTransform _rectTransform;
    private Vector2 _visibleAnchorsMin, _visibleAnchorsMax, _hiddenAnchorsMin, _hiddenAnchorsMax;
    private Vector2 _targetMin, _targetMax;
    private Rect _targetRect;
    private bool _changedState, _updating;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _targetRect = new Rect(_rectTransform.rect);
        _targetRect.xMin = 0;
        _targetRect.xMax = 0;
        _targetRect.yMin = 0;
        _targetRect.yMax = 0;

        _visibleAnchorsMin = _rectTransform.anchorMin;
        _visibleAnchorsMax = _rectTransform.anchorMax;

        _hiddenAnchorsMin = _hiddenAnchorsMax = Vector2.Lerp(_visibleAnchorsMin, _visibleAnchorsMax, 0.5f);

        _updating = false;

        if (CanvasGroup == null)
            Debug.LogWarning("PanelZoomInOut: No CanvasGroup detected. This componenet will still work, however, no child content will be hidden when the component is hidden.");
    }

    void Start()
    {
        StartCoroutine(CheckChangeInState());
    }

    private IEnumerator CheckChangeInState()
    {
        var oldCheckRate = int.MinValue;
        var refreshSec = 1f;
        var oldState = !Visible;
        Coroutine oldUpdateAnchors = null;

        while (true)
        {
            if (oldState != Visible)
            {
                oldState = Visible;
                if (!Visible && CanvasGroup != null)
                {
                    CanvasGroup.alpha = 0;
                }

                _targetMin = Visible ? _visibleAnchorsMin : _hiddenAnchorsMin;
                _targetMax = Visible ? _visibleAnchorsMax : _hiddenAnchorsMax;

                if (_updating && oldUpdateAnchors != null)
                    StopCoroutine(oldUpdateAnchors);
                oldUpdateAnchors = StartCoroutine(UpdateAnchors());
            }
            else if (!_updating && Visible)
            {
                if (CanvasGroup != null)
                    CanvasGroup.alpha = 1;
            }

            if (oldCheckRate != CheckRate)
            {
                oldCheckRate = CheckRate;
                refreshSec = 1f / CheckRate;
            }

            yield return new WaitForSeconds(refreshSec);
        }
    }

    private IEnumerator UpdateAnchors()
    {
        var updatedMin = false;
        var updatedMax = false;
        _updating = true;

        while (!updatedMin || !updatedMax)
        {
            var newAnchorsMin = Vector2.zero;
            var newAnchorsMax = Vector2.zero;

            if (Vector2.Distance(_rectTransform.anchorMin, _targetMin) > 0.01f)
            {
                newAnchorsMin = Vector2.Lerp(_rectTransform.anchorMin, _targetMin, Time.unscaledDeltaTime * AnimationSpeed);
            }
            else if(!updatedMin)
            {
                updatedMin = true;
                newAnchorsMin = _targetMin;
            }

            if (Vector2.Distance(_rectTransform.anchorMax, _targetMax) > 0.01f)
            {
                newAnchorsMax = Vector2.Lerp(_rectTransform.anchorMax, _targetMax, Time.unscaledDeltaTime * AnimationSpeed);
            }
            else if(!updatedMax)
            {
                updatedMax = true;
                newAnchorsMax = _targetMax;
            }

            _rectTransform.anchorMin = newAnchorsMin;
            _rectTransform.anchorMax = newAnchorsMax;

            yield return null;
        }

        _updating = false;
    }
}
