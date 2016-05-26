using UnityEngine;
using System.Collections;

public abstract class GameMenuManager : MenuManager
{
    public enum Actions
    {
        Pause,
        Resume,
        LoadLastCheckpoint,
        Settings,
        Back,
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
        LeaveShop
    }

    public bool PauseGame = true;
    [Tooltip("Only gets applied if PauseGame is set to false")]
    public bool BlockGameplayInput = true;
    public Axis OpenKey;
    public GameMenuTransition[] Transitions;
    protected GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    public override void HandleInput(bool active)
    {
        if (active)
        {
            if (PlayerInput.IsButtonUp(BackKey))
            {
                OnBackPressed();
            }
        }
        else if (!_gameManager.GamePaused)
        {
            if (PlayerInput.IsButtonUp(OpenKey))
            {
                OnOpen();
            }            
        }
    }

    protected virtual void OnOpen()
    {
        OnPause(true);
    }

    public void OnBackPressed()
    {
        OnNavItemSelected(null, Actions.Back, null);
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        if (NavMenu.IsSubMenu)
        {
            if (value)
            {
                OnPause(value);
            }
        }
        else
        {
            OnPause(value);
        }
    }

    protected void OnPause(bool value)
    {
        if (PauseGame)
        {
            _gameManager.SetGamePaused(value);
        }
        else if(BlockGameplayInput)
        {
            _gameManager.BlockGameplayInput(value);
        }
        NavMenu.SetActive(value);
    }

    public override void OnNavItemSelected(NavItem item, object actionObj, object dataObj)
    {
        var action = (Actions)actionObj;
        var target = Transitions.Find(action);
        var data = dataObj as string[];

        OnNavItemAction(action, item, target, data);
    }

    protected override void OnNavItemAction(object actionObj, NavItem navItem, NavMenu targetNavMenu, string[] data)
    {
        base.OnNavItemAction(actionObj, navItem, targetNavMenu, data);
    }

    private void OnValidate()
    {
        if (Transitions.Length > 0)
        {
            foreach (var t in Transitions)
            {
                t.Name = t.OnAction.ToString();
            }
        }
    }

    [System.Serializable]
    public class GameMenuTransition
    {
        [HideInInspector]
        public string Name;
        public Actions OnAction;
        public NavMenu TargetMenu;
    }
}

public static class GameMenuTransitionHelper
{
    public static NavMenu Find(this GameMenuManager.GameMenuTransition[] transitions, GameMenuManager.Actions action)
    {
        foreach (var transition in transitions)
        {
            if (transition.OnAction == action)
            {
                return transition.TargetMenu;
            }
        }

        return null;
    }
}