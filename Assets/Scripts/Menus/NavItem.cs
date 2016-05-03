using UnityEngine;
using System.Collections;

public abstract class NavItem : MonoBehaviour
{
    [SerializeField]
    protected bool _selected;

    public void Focus(bool value)
    {
        _selected = value;
        OnFocus(value);
    }

    protected abstract void OnFocus(bool value);

    public abstract void OnSelect(MainMenuManager manager);
}
