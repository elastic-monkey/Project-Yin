using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMenu : Menu
{
    public Axes OpenKey = Axes.None;
    public Axes CloseKey = Axes.None;

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

    protected override void Update()
    {
        base.Update();

        SetBackText("Close", CloseKey);

        if (IsOpen)
        {
            if (CloseKey != Axes.None && PlayerInput.IsButtonDown(CloseKey))
            {
                Close();
            }
        }

        if (GameManager.IsGamePaused)
            return;

        if (OpenKey != Axes.None && PlayerInput.IsButtonDown(OpenKey))
        {
            Open();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();

        GameManager.SetGamePaused(false);
    }

    public override void Open()
    {
        base.Open();

        SoundManager.PlayOpenSound();
        GameManager.SetGamePaused(true);
    }

    public override void Close()
    {
        base.Close();

        SoundManager.PlayCloseSound();
    }

    public override void OnNavItemFocused(NavItem target, bool silent = false)
    {
        if (!IsOpen)
            return;

        if (!silent)
            SoundManager.PlayFocusItemSound();
    }
}