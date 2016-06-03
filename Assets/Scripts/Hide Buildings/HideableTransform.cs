using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideableTransform : MonoBehaviour, IHideable
{
    public Transform[] MeshesToHide;
    public Collider[] Colliders;
    public float CombinedHeight = 5.0f;
    public float VisibleHeight = 1.0f;
    public float AnimDuration = 1.0f;

    private Coroutine _lastHide = null;
    private List<Vector3> _initialPositions, _hiddenPositions;
    [SerializeField]
    private bool _hidden;

    private void Start()
    {
        _initialPositions = new List<Vector3>();
        _hiddenPositions = new List<Vector3>();

        var hiddenOffset = new Vector3(0, CombinedHeight - VisibleHeight, 0);
        foreach (var mesh in MeshesToHide)
        {
            _initialPositions.Add(mesh.localPosition);
            _hiddenPositions.Add(mesh.localPosition - hiddenOffset);
        }

        foreach (var col in Colliders)
        {
            col.gameObject.layer = LayerMask.NameToLayer("Hideable Building");
        }

        if (_hidden)
            Hide();
        else
            Show();
    }

    public void Hide()
    {
        if (_hidden)
            return;

        if (_lastHide != null)
            StopCoroutine(_lastHide);

        _lastHide = StartCoroutine(HideCoroutine(true));
    }

    public void Show()
    {
        if (!_hidden)
            return;
        
        if (_lastHide != null)
            StopCoroutine(_lastHide);

        _lastHide = StartCoroutine(HideCoroutine(false));
    }

    public bool IsHidden()
    {
        return _hidden;
    }

    private IEnumerator HideCoroutine(bool hide)
    {
        var timePassed = 0.0f;
        var invDuration = 1.0f / AnimDuration;
        _hidden = hide;

        while (timePassed <= AnimDuration)
        {
            LerpPositions(timePassed * invDuration, hide);
            timePassed += Time.deltaTime;

            yield return null;
        }

        LerpPositions(1.0f, hide);
    }

    private void LerpPositions(float lerpFactor, bool hide)
    {
        for (var i = 0; i < MeshesToHide.Length; i++)
        {
            var mesh = MeshesToHide[i];
            var targetPos = hide ? _hiddenPositions[i] : _initialPositions[i];
            mesh.localPosition = Vector3.Lerp(mesh.localPosition, targetPos, lerpFactor);
        }
    }
}
