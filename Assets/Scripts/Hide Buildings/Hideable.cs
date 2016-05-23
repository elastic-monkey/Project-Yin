using UnityEngine;
using System.Collections;

public class Hideable : MonoBehaviour
{
    public static LayerMask mask = LayerMask.NameToLayer("Hideable Building");

    public Collider ColliderArea;
    public Mesh MeshToHide;

    private void Awake()
    {
        ColliderArea.isTrigger = true;
        ColliderArea.gameObject.layer = mask;
    }
}
