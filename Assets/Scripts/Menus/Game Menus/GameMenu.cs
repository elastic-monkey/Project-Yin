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
    public int MenuStackCount = 0;

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

    private void Update()
    {
        MenuStackCount = _navMenusHistory.Count;

        if (IsOpen)
        {
            if (CloseKey != Axes.None && PlayerInput.IsButtonDown(CloseKey))
            {
                Close();
            }
        }
        else
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
            _navMenusHistory.Peek().SetActive(true);
        }
    }

    public void ChangeTo(NavMenu target)
    {
        if (!NavMenus.Contains(target))
            return;

        _navMenusHistory.Peek().SetActive(false);
        _navMenusHistory.Push(target);
        target.SetActive(true);
    }

    public virtual void OnNavItemFocused(NavItem target)
    {
        if (!IsOpen)
            return;
        
		SoundManager.PlayFocusItemSound();
	}
}