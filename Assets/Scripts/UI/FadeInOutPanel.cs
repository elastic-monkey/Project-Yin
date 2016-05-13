﻿using UnityEngine;
using System.Collections;

public class FadeInOutPanel : MonoBehaviour, IAnimatedPanel
{
    public CanvasGroup AlphaContainer;
    public float Duration;

    private Coroutine _oldAnim;

    public void SetVisible(bool value)
    {
        if (_oldAnim != null)
            StopCoroutine(_oldAnim);

        _oldAnim = StartCoroutine(AnimateCoroutine(value));
    }

    private IEnumerator AnimateCoroutine(bool value)
    {
        var targetAlpha = value ? 1 : 0;
        var timePassed = 0f;

        while (timePassed <= 1)
        {
            timePassed += Time.unscaledDeltaTime;

            AlphaContainer.alpha = Mathf.Lerp(AlphaContainer.alpha, targetAlpha, timePassed / Duration);

            yield return null;
        }

        AlphaContainer.alpha = targetAlpha;
    }
}