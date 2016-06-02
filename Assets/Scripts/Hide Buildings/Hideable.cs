using UnityEngine;
using System.Collections;

public class Hideable : MonoBehaviour
{
    public Collider ColliderArea;
    public Transform MeshToHide;
    public Transform SubstituteMesh;

    private bool _hidden;
    private Renderer _meshToHide, _substituteMesh;

    public bool Hidden
    {
        get { return _hidden; }

        set
        {
            if (_hidden == value)
                return;
            
            _hidden = value;
            OnHide(value);
        }
    }

    private void Awake()
    {
        _meshToHide = MeshToHide.GetComponent<Renderer>();
        if (SubstituteMesh != null)
        {
            _substituteMesh = SubstituteMesh.GetComponent<Renderer>();
        }
    }

    protected virtual void Start()
    {
        ColliderArea.gameObject.layer = LayerMask.NameToLayer("Hideable Building");   
    }

    protected virtual void OnHide(bool value)
    {
        _meshToHide.enabled = !value;
        if (_substituteMesh != null)
        {
            _substituteMesh.enabled = value;
        }
    }
}
