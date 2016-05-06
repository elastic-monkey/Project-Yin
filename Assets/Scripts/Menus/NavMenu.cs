using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IAnimatedPanel))]
public abstract class NavMenu : MonoBehaviour
{
    [SerializeField]
    private bool _active;
    private bool _activeNextFrame;
    private IAnimatedPanel _animatedPanel;

    public bool IsActive
    {
        get
        {
            return _active;
        }
    }

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
    }

    private void Update()
    {
        if (_activeNextFrame)
        {
            _activeNextFrame = false;
            _active = true;
            return;
        }

        if (!_active)
            return;

        OnUpdate();
    }

    public void SetActive(bool value)
    {
        _activeNextFrame = value;
        _active = false;

        _animatedPanel.SetVisible(value);

        OnSetActive(value);
    }

    protected abstract void OnUpdate();

    protected abstract void OnSetActive(bool value);
}
