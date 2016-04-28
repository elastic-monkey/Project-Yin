using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hideable : MonoBehaviour
{
    public float Speed = 10;
    public float HiddenBaseHeight = 1;
    public bool Hidden = false;

    public HideableObj[] _children;

    public Coroutine _update;

    private void Awake()
    {
        _children = new HideableObj[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var visiblePos = child.localPosition;
            var hiddenY = (-transform.localScale.y * child.localPosition.y + HiddenBaseHeight - transform.localPosition.y) / transform.localScale.y;
            var hiddenPos = new Vector3(visiblePos.x, hiddenY, visiblePos.z);
            _children[i] = new HideableObj(child, visiblePos, hiddenPos);
        }
    } 

    public void Hide()
    {
        if (!Hidden)
        {
            Hidden = true;

            foreach (var child in _children)
            {
                if (child.LastCoroutine.Exists())
                    StopCoroutine(child.LastCoroutine);

                child.LastCoroutine = StartCoroutine(UpdateCoroutine(child.Transform, child.HiddenPos));
            }
        }
    }

    public void Show()
    {
        if (Hidden)
        {
            Hidden = false;

            foreach (var child in _children)
            {
                if (child.LastCoroutine.Exists())
                    StopCoroutine(child.LastCoroutine);

                child.LastCoroutine = StartCoroutine(UpdateCoroutine(child.Transform, child.VisiblePos));
            }
        }
    }

    private IEnumerator UpdateCoroutine(Transform target, Vector3 targetPos)
    {
        while (Vector3.Distance(target.localPosition, targetPos) > 0.05f)
        {
            target.localPosition = Vector3.Lerp(target.localPosition, targetPos, Time.deltaTime * Speed);

            yield return null;
        }

        target.localPosition = targetPos;
    }
}

public class HideableObj
{
    public Transform Transform;
    public Vector3 VisiblePos, HiddenPos;
    public Coroutine LastCoroutine;

    public HideableObj(Transform t, Vector3 visible, Vector3 hidden)
    {
        Transform = t;
        VisiblePos = visible;
        HiddenPos = hidden;
    }
}