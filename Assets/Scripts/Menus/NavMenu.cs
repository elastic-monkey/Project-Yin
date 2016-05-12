using UnityEngine;
using System.Collections;

public abstract class NavMenu : MonoBehaviour
{
    public MenuManager MenuManager;

    [SerializeField]
    private bool _active;
    private bool _activeNextFrame;
    [SerializeField]
    private bool _inputBlocked;
    private bool _inputUnlockedNextFrame;
    private IAnimatedPanel _animatedPanel;

    public bool InputBlocked
    {
        get
        {
            return _inputBlocked;
        }

        set
        {
            if (value)
            {
                _inputBlocked = true;
                _inputUnlockedNextFrame = false;
            }
            else
            {
                _inputUnlockedNextFrame = true;
            }
        }
    }

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
        if (!_active)
            return;

        if (InputBlocked)
            return;

        MenuManager.HandleInput();

        OnUpdate();
    }

    private void LateUpdate()
    {
        if (_activeNextFrame)
        {
            _activeNextFrame = false;
            _active = true;
        }

        if (_inputUnlockedNextFrame)
        {
            _inputBlocked = false;
            _inputUnlockedNextFrame = false;
        }
    }

    public void SetActive(bool value)
    {
        _activeNextFrame = value;
        _active = false;

        if (_animatedPanel != null)
        {
            _animatedPanel.SetVisible(value);
        }

        OnSetActive(value);
    }

    public abstract void UnfocusAll();

    public abstract void FocusCurrent();

    protected abstract void OnUpdate();

    protected abstract void OnSetActive(bool value);
}
