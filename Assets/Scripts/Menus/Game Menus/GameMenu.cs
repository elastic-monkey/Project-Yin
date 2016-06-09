using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour, IMenu
{
    public enum Actions
    {
        Pause,
        Resume,
        LoadLastCheckpoint,
        Settings,
        Close,
        UpgradeHealth,
        UpgradeStamina,
        UpgradeSpeed,
        UpgradeShield,
        UpgradeStrength,
        ConfirmSave,
        RefuseSave,
        OpenDialog,
        GoToUpgradeMenu,
        GoToInventoryMenu,
        UseItem,
        BuyItem,
        GoToShop,
        GoToSellComponents,
        SellComponents,
        LeaveShop,
        GoToMainMenu
    }

    public Axes OpenKey;
    public Axes CloseKey;
    public bool IsOpen = false;
    public bool SubMenu = false;
    public bool PausesGame = true;
    public bool BlockGameplayInput = true;
    public GameMenuTransition[] Transitions;
    private GameManager _gameManager;
    private NavMenu _navMenu;

    public NavMenu NavMenu
    {
        get
        {
            if (_navMenu == null)
                _navMenu = GetComponent<NavMenu>();

            return _navMenu;
        }
    }

    protected GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;

            return _gameManager;
        }
    }

    private void Update()
    {
        if (PlayerInput.OnlyMenus && !IsOpen)
            return;

        if (!IsOpen && PlayerInput.IsButtonUp(OpenKey))
        {
            Open();
        }
        else if (IsOpen && PlayerInput.IsButtonUp(CloseKey))
        {
            Close();
        }
    }

    public virtual void OnNavItemFocused(NavItem target)
    {
        // Do stuff
    }

    public virtual void OnNavItemSelected(NavItem item, object actionObj, string[] dataObj)
    {
        if (OnNavItemAction(item, actionObj, dataObj))
            return;

        TransitionTo(Transitions.Find((Actions)actionObj));
    }

    public virtual bool OnNavItemAction(NavItem navItem, object actionObj, string[] data)
    {
        return false;
    }

    public virtual void Open()
    {
        IsOpen = true;
        NavMenu.SetActive(true);

        Pause(true);
    }

    public virtual void Close()
    {
        IsOpen = false;
        NavMenu.SetActive(false);

        if (SubMenu)
            TransitionTo(Transitions.Find(Actions.Close));
        else
            Pause(false);
    }

    protected void TransitionTo(GameMenu other)
    {
        if (other == null)
            return;

        other.Open();
        other.NavMenu.InputBlocked = false;

        if (IsOpen && other.SubMenu)
        {
            NavMenu.InputBlocked = true;
            NavMenu.UnfocusAll();
            IsOpen = false;
        }
    }

    protected void Pause(bool value)
    {
        if (PausesGame)
        {
            GameManager.SetGamePaused(value);
        }
        else if (BlockGameplayInput)
        {
            GameManager.BlockGameplayInput(value);
        }
        
        NavMenu.SetActive(value);
    }

    private void OnValidate()
    {
        if (Transitions.Length > 0)
            foreach (var t in Transitions)
                t.Name = t.OnAction.ToString();
    }

    [System.Serializable]
    public class GameMenuTransition
    {
        [HideInInspector]
        public string Name;
        public Actions OnAction;
        public GameMenu TargetMenu;
    }
}

public static class GameMenuTransitionHelper
{
    public static GameMenu Find(this GameMenu.GameMenuTransition[] transitions, GameMenu.Actions action)
    {
        foreach (var transition in transitions)
            if (transition.OnAction == action)
                return transition.TargetMenu;

        return null;
    }
}