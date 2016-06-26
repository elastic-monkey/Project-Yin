using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Menu : MonoBehaviour
{
    public int DefaultNavMenu;
    public List<NavMenu> NavMenus;
    public bool IsOpen = false;

    private Stack<NavMenu> _navMenusHistory;
    private bool _openNextFrame = false, _closeNextFrame = false;

    protected NavMenu CurrentNavMenu
    {
        get
        {
            if (_navMenusHistory == null || _navMenusHistory.Count == 0)
                return null;

            return _navMenusHistory.Peek();
        }
    }

    protected virtual void Awake()
    {
        _navMenusHistory = new Stack<NavMenu>();
    }

    protected virtual void Start()
    {
        // Do stuff
    }

    protected virtual void Update()
    {
        // Do stuff
    }

    protected virtual void LateUpdate()
    {
        if (_openNextFrame)
        {
            _openNextFrame = false;
            IsOpen = true;
        }

        if (_closeNextFrame)
        {
            _closeNextFrame = false;
            IsOpen = false;
            OnClose();
        }
    }

    protected virtual void OnClose()
    {
        // Do stuff
    }

    public virtual void Open()
    {
        _openNextFrame = true;
        _navMenusHistory.Push(NavMenus[DefaultNavMenu]);
        NavMenus[DefaultNavMenu].SetActive(true);
    }

    public virtual void Close()
    {
        _navMenusHistory.Pop().SetActive(false);

        if (_navMenusHistory.Count == 0)
        {
            _closeNextFrame = true;
        }
        else
        {
            CurrentNavMenu.InputBlocked = false;
            CurrentNavMenu.SetActive(true);
        }
    }

    public bool CloseIfSubmenu()
    {
        if (_navMenusHistory.Count > 1)
        {
            Close();
            return true;
        }

        return false;
    }

    public virtual void ChangeTo(NavMenu target, bool submenu = true)
    {
        if (NavMenus.Contains(target))
        {
            if (target == CurrentNavMenu)
                return;

            if (!submenu)
            {
                CurrentNavMenu.SetActive(false);
            }
            else
            {
                CurrentNavMenu.InputBlocked = true;
            }

            _navMenusHistory.Push(target);
            target.SetActive(true);
        }
        else // Change to outer menu
        {
            while (_navMenusHistory.Count > 1)
            {
                _navMenusHistory.Pop().SetActive(false);
            }
            Close();

            target.Menu.Open();
        }
    }

    public abstract void OnNavItemFocused(NavItem target, bool silent = false);

    public virtual void QuitGame()
    {
        Application.Quit();
    }
}
