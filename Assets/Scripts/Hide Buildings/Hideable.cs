using UnityEngine;
using System.Collections;

public class Hideable : MonoBehaviour
{
    public Collider ColliderArea;
    public MeshRenderer MeshToHide;
    public MeshRenderer SubstituteMesh;

    private bool _hidden;

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

    private void Start()
    {
        ColliderArea.gameObject.layer = LayerMask.NameToLayer("Hideable Building");   
    }

    protected virtual void OnHide(bool value)
    {
        MeshToHide.enabled = !value;
        if (SubstituteMesh != null)
        {
            SubstituteMesh.enabled = value;
        }
    }
}
