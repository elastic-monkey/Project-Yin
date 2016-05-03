using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : NavMenu
{
    public enum Options
    {
        NewGame,
        LoadGame,
        Settings,
        Quit
    }

    public MainMenuItems[] Items;

    [SerializeField]
    private int _currentOption;
    private MainMenuManager _mainMenuManager;

    protected override void Awake()
    {
        base.Awake();

        _mainMenuManager = GetComponentInParent<MainMenuManager>();
    }

    protected void Start()
    {
        SelectOption((Options)0);
    }

    public override void HandleInput()
    {
        if (!_active)
            return;

        if (PlayerInput.IsButtonDown(Axis.Nav_Vertical))
        {
            var v = -PlayerInput.GetAxis(Axis.Nav_Vertical);

            if (v > 0)
            {
                SelectNext();
            }
            else if (v < 0)
            {
                SelectPrevious();
            }
        }
        else if (PlayerInput.IsButtonUp(Axis.Fire1) || PlayerInput.IsButtonUp(Axis.Submit))
        {
            switch (Items[_currentOption].Option)
            {
                case Options.LoadGame:
                    _mainMenuManager.SelectMenu(MainMenuManager.Options.LoadMenu);
                    break;
            }
        }
    }

    private void SelectOption(Options newOption)
    {
        for (var i = 0; i < Items.Length; i++)
        {
            if (Items[i].Option == newOption)
            {
                _currentOption = i;
                Items[i].Item.Select(true);
            }
            else
            {
                Items[i].Item.Select(false);
            }
        }
    }

    private void SelectNext()
    {
        if (_currentOption < Items.Length - 1)
        {
            SelectOption((Options)(_currentOption + 1));
        }
        else
        {
            if (Cyclic)
            {
                SelectOption((Options)0);
            } 
        }
    }

    private void SelectPrevious()
    {
        if (_currentOption > 0)
        {
            SelectOption((Options)(_currentOption - 1));
        }
        else
        {
            if (Cyclic)
            {
                SelectOption((Options)(Items.Length - 1));
            } 
        } 
    }

    [System.Serializable]
    public class MainMenuItems
    {
        public Options Option;
        public NavItem Item;
    }

    protected override void OnSetActive(bool value)
    {
        SelectOption(Options.NewGame);
    }
}