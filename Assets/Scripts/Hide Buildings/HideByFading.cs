using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideByFading : MonoBehaviour, IHideable
{
    public float Duration = 1.0f;
    public Collider[] Colliders;
    [HideInInspector]
    public List<Renderer> Renderers;

    private bool _hidden;
    private SwapToFadeManager _swapManager;
    private Coroutine _lastCoroutine;

    public void Awake()
    {
        Renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());

        _swapManager = GameManager.Instance.SwapFadeMaterials;

        foreach (var col in Colliders)
        {
            col.gameObject.layer = LayerMask.NameToLayer("Hideable Building");
        }
    }

    public void Hide()
    {
        if (_hidden)
            return;

        if (_lastCoroutine != null)
            StopCoroutine(_lastCoroutine);

        _lastCoroutine = StartCoroutine(HideCoroutine(true));
    }

    public void Show()
    {
        if (!_hidden)
            return;

        if (_lastCoroutine != null)
            StopCoroutine(_lastCoroutine);

        _lastCoroutine = StartCoroutine(HideCoroutine(false));
    }

    public bool IsHidden()
    {
        return _hidden;
    }

    private IEnumerator HideCoroutine(bool hide)
    {
        _hidden = hide;

        var targetAlpha = hide ? 0f : 1f;
        var timePassed = 0f;
        var invDuration = 1f / Duration;

        if (hide)
        {
            _swapManager.ReplaceByFadeMaterials(this);

            while (timePassed <= Duration)
            {
                SetFade(targetAlpha, timePassed * invDuration);

                yield return null;

                timePassed += Time.deltaTime;
            }
        }
        else
        {
            while (timePassed <= Duration)
            {
                SetFade(targetAlpha, timePassed * invDuration);

                yield return null;

                timePassed += Time.deltaTime;
            }

            _swapManager.ReplaceByOpaqueMaterials(this);
        }
    }

    private void SetFade(float targetValue, float lerpFactor)
    {
        foreach (var r in Renderers)
        {
            var mats = r.materials;
            foreach (var mat in mats)
            {
                var c = mat.color;
                c.a = Mathf.Lerp(targetValue == 0f ? 1f : 0f, targetValue, lerpFactor);
                mat.color = c;
            }
            r.materials = mats;
        }
    }
//
//    private void ReplaceToFadeMaterials()
//    {
//        foreach (var r in _renderers)
//        {
//            var mats = r.sharedMaterials;
//            for(var i = 0; i < mats.Length; i++)
//            {
//                var swap = _swapManager.FindSubstitute(mats[i]);
//                mats[i] = swap;
//            }
//            r.materials = mats;
//        }
//    }
//
//    private void ReplaceToOpaqueMaterials()
//    {
//        foreach (var r in _renderers)
//        {
//            var mats = r.sharedMaterials;
//            for (var i = 0; i < mats.Length; i++)
//            {
//                var swap = _swapManager.FindOriginal(mats[i]);
//                mats[i] = swap;
//            }
//            r.materials = mats;
//        }
//    }
}
