﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MenuManager
{
    public enum Actions
    {
        NewGame,
        Settings,
        ChangeMenu,
    }

    public MainMenuChangeMenuNavItem OnBackPressed;

    private void Start()
    {
        NavMenu.SetActive(NavMenu.IsActive);
    }

    private void Update()
    {
        if (!NavMenu.IsActive)
            return;
        
        if (PlayerInput.IsButtonUp(Axis.Escape))
        {
            OnAction(OnBackPressed, OnBackPressed.Action, OnBackPressed.Target);
        }
    }

    public override void OnFocus(NavItem target)
    {
        // Do stuff
    }

    public override void OnAction(NavItem item, object action, object data)
    {
        var actionEnum = (Actions)action;
    
        switch (actionEnum)
        {
            case Actions.NewGame:
                SceneManager.LoadScene("Level_1");
                break;

            case Actions.Settings:
                break;

            case Actions.ChangeMenu:
                var target = data as NavMenu;
                if(target != null)
                {
                    target.SetActive(true);
                    NavMenu.SetActive(false);               
                }
                break;
        }
    }
}
