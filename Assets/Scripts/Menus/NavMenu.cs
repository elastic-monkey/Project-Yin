using UnityEngine;
using System.Collections;

public abstract class NavMenu : MonoBehaviour
{
    public MenuManager MenuManager;
    public bool IsSubMenu;

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

    private void Start()
    {
        OnSetActive(IsActive);
    }

    private void Update()
    {
        if (InputBlocked)
            return;

        MenuManager.HandleInput(IsActive);

        if (!IsActive)
            return;

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

    public virtual void OnSetActive(bool value)
    {
        _activeNextFrame = value;
        _active = false;

        if (_animatedPanel != null)
        {
            _animatedPanel.SetVisible(value);
        }

        FocusCurrent();
    }

    public abstract void UnfocusAll();

    public abstract void FocusCurrent();

    protected abstract void OnUpdate();
}
