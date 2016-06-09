using UnityEngine;

public abstract class NavMenu : MonoBehaviour
{
    public RectTransform HoverIcon;
    public bool IsSubMenu;
    public bool UseHoverNavigation;
    public bool Cyclic, Reset;

    [SerializeField]
    private bool _active;
    private bool _activeNextFrame;
    [SerializeField]
    private bool _inputBlocked;
    private bool _inputUnlockedNextFrame;
    private IAnimatedPanel _animatedPanel;
    private Menu _menuManager;

    public Menu MenuManager
    {
        get
        {
            if (_menuManager == null)
                _menuManager = GetComponent<Menu>();

            return _menuManager;
        }
    }

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
        SetActive(IsActive);
    }

    private void Update()
    {
        if (!IsActive || InputBlocked)
            return;

        HandleInput();
    }

    private void LateUpdate()
    {
        if (_activeNextFrame)
        {
            _activeNextFrame = false;
            _active = true;
            _inputBlocked = false;
        }

        if (_inputUnlockedNextFrame)
        {
            _inputBlocked = false;
            _inputUnlockedNextFrame = false;
        }
    }

    public virtual void SetActive(bool value)
    {
        _activeNextFrame = value;
        _active = false;

        if (_animatedPanel != null)
            _animatedPanel.SetVisible(value);

        if (HoverIcon != null)
            HoverIcon.gameObject.SetActive(value);

        FocusCurrent();
    }

    protected virtual void FocusItem(NavItem item)
    {
        MenuManager.OnNavItemFocused(item);

        if (HoverIcon != null)
        {
            if (UseHoverNavigation)
            {
                HoverIcon.gameObject.SetActive(true);
                HoverIcon.SetParent(item.transform, false);
            }
            else
            {
                HoverIcon.gameObject.SetActive(false);
            }         
        }
    }

    public abstract void UnfocusAll();

    public abstract void FocusCurrent();

    protected abstract void HandleInput();
}
