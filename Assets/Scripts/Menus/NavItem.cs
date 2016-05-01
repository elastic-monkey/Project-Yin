using UnityEngine;
using System.Collections;

public abstract class NavItem : MonoBehaviour
{
    [SerializeField]
    protected bool _selected;

    public void Select(bool value)
    {
        _selected = value;
        OnSelect(value);
    }

    protected abstract void OnSelect(bool value);
}
