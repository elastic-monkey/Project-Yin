using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IAnimatedPanel))]
public abstract class NavMenu : MonoBehaviour
{
    public bool Cyclic;

    [SerializeField]
    protected bool _active;

    protected IAnimatedPanel _animatedPanel;

    public abstract void HandleInput();

    protected virtual void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
    }

    public void SetActive(bool value)
    {
        _active = value;
        _animatedPanel.SetVisible(_active);
    }

    protected abstract void OnSetActive(bool value);
}
