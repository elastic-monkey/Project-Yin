using UnityEngine;
using System.Collections.Generic;

public abstract class NavMenu : MonoBehaviour
{
    public RectTransform HoverIcon;
    public bool UseHoverNavigation;
    public bool Cyclic, Reset;
    public bool IsActive;

    private bool _activeNextFrame;
    [SerializeField]
    private bool _inputBlocked;
    private bool _inputUnlockedNextFrame;
    private IAnimatedPanel _animatedPanel;
    private Menu _menu;

    public Menu Menu
    {
        get
        {
            if (_menu == null)
                _menu = GetComponent<Menu>();

            if (_menu == null)
                _menu = GetComponentInParent<Menu>();

            return _menu;
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
            IsActive = true;
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
        if (value == IsActive)
            return;
        
        _activeNextFrame = value;
        IsActive = false;
        if (value)
            InputBlocked = false;

        if (_animatedPanel != null)
            _animatedPanel.SetVisible(value);

        if (HoverIcon != null)
            HoverIcon.gameObject.SetActive(value);

        FocusCurrent();
    }

    protected virtual void FocusItem(NavItem item)
    {
        Menu.OnNavItemFocused(item);

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

    public abstract List<NavItem> GetNavItems();

    public abstract NavItem GetCurrentNavItem();
}
