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
		RefuseSave
    }

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
            if (PlayerInput.IsButtonUp(BackKey) && active)
            {
                OnPause(false);
            }
        }
        else if(!_gameManager.GamePaused)
        {
            if (PlayerInput.IsButtonUp(OpenKey))
            {
                OnPause(true);
            }            
        }
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        OnPause(value);
    }

    protected void OnPause(bool value)
    {
        _gameManager.SetGamePaused(value);
        NavMenu.OnSetActive(value);
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

        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.Back:
                OnPause(false);
                break;
        }
    }
}

[System.Serializable]
public class GameMenuTransition
{
    public string Name;
    public GameMenuManager.Actions OnAction;
    public NavMenu TargetMenu;   
}

public static class GameMenuTransitionHelper
{
    public static NavMenu Find(this GameMenuTransition[] transitions, GameMenuManager.Actions action)
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