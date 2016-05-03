using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    public enum Options
    {
        MainMenu,
        LoadMenu,
        QuitMenu
    }

    public MainMenuItems[] Items;
    public bool Active;

    [SerializeField]
    private int _currentItem;

    public Options CurrentMenuOption
    {
        get
        {
            return Items[_currentItem].Option;
        }
    }

    private void Start()
    {
        Active = true;
        SelectMenu(Options.MainMenu);
    }

    private void Update()
    {
        if (!Active)
            return;

        PlayerInput.GameplayBlocked = true;

        if (PlayerInput.IsButtonUp(Axis.Escape))
        {
            OnBackPressed();
        }

        Items[_currentItem].Menu.HandleInput();
    }

    public void SelectMenu(Options option)
    {
        for (var i = 0; i < Items.Length; i++)
        {
            if (Items[i].Option == option)
            {
                _currentItem = i;
                Items[i].Menu.SetActive(true);
            }
            else
            {
                Items[i].Menu.SetActive(false);
            }
        }
    }

    public void OnBackPressed()
    {
        if (CurrentMenuOption == Options.MainMenu)
            OnQuitPressed();
        else
            SelectMenu(Options.MainMenu);
    }

    public void OnQuitPressed()
    {
        SelectMenu(Options.QuitMenu);
    }

    public void OnQuitCancelled()
    {
        SelectMenu(Options.MainMenu);
    }

    public void OnQuitConfirmed()
    {
        Application.Quit();
    }

    [System.Serializable]
    public class MainMenuItems
    {
        public Options Option;
        public NavMenu Menu;
    }
}
