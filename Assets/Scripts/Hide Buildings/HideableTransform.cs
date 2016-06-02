using UnityEngine;
using System.Collections;

public class HideableTransform : Hideable
{
    public float Height = 5.0f;
    public float VisibleHeight = 1.0f;
    public float AnimDuration = 1.0f;

    private Coroutine _lastHide = null;
    private Vector3 _initialPosition, _hiddenPosition;

    protected override void Start()
    {
        base.Start();
    
        _initialPosition = MeshToHide.transform.localPosition;
        _hiddenPosition = _initialPosition - new Vector3(0, Height - VisibleHeight, 0);
    }

    protected override void OnHide(bool value)
    {
        if (_lastHide != null)
            StopCoroutine(_lastHide);

        _lastHide = StartCoroutine(HideCoroutine(value));
    }

    private IEnumerator HideCoroutine(bool value)
    {
        var timePassed = 0f;
        var invDuration = 1f / AnimDuration;
        var targetPosition = value ? _hiddenPosition : _initialPosition;

        while (timePassed <= AnimDuration)
        {
            var lerpFactor = timePassed * invDuration;
            MeshToHide.transform.localPosition = Vector3.Lerp(MeshToHide.transform.localPosition, targetPosition, lerpFactor);

            timePassed += Time.deltaTime;
            yield return null;
        }

        MeshToHide.transform.localPosition = targetPosition;
    }
}
