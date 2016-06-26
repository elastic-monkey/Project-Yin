using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMenu : MonoBehaviour
{
    public int DefaultNavMenu;
    public List<NavMenu> NavMenus;
    public Axes OpenKey = Axes.None;
    public Axes CloseKey = Axes.None;
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

    protected GameManager GameManager
    {
        get
        {
            return GameManager.Instance;
        }
    }

	protected MenuSoundManager SoundManager
	{
		get
		{
            return GameManager.MenuSoundManager;
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

    private void Update()
    {
        if (IsOpen)
        {
            if (CloseKey != Axes.None && PlayerInput.IsButtonDown(CloseKey))
            {
                Close();
            }
        }
        else if(!GameManager.IsGamePaused)
        {
            if (OpenKey != Axes.None && PlayerInput.IsButtonDown(OpenKey))
            {
                Open();
            }
        }
    }

	private void LateUpdate()
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
            GameManager.SetGamePaused(false);
        }
	}

    public virtual void Open()
    {
        _openNextFrame = true;
        SoundManager.PlayOpenSound();
        GameManager.SetGamePaused(true);
        _navMenusHistory.Push(NavMenus[DefaultNavMenu]);
        NavMenus[DefaultNavMenu].SetActive(true);
    }

    public virtual void Close()
    {
        _navMenusHistory.Pop().SetActive(false);
        SoundManager.PlayCloseSound();

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
            while (_navMenusHistory.Count > 0)
            {
                _navMenusHistory.Pop().SetActive(false);
            }
            Close();

            target.Menu.Open();
        }
    }

    public virtual void OnNavItemFocused(NavItem target)
    {
        if (!IsOpen)
            return;
        
		SoundManager.PlayFocusItemSound();
	}
}