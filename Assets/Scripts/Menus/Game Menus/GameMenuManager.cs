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
        UpgradeStrength
    }

    public Axis OpenKey;
    public Actions OnBack;
    public GameMenuTransition[] Transitions;
    protected GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void LateUpdate()
    {
        if (PlayerInput.IsButtonUp(OpenKey))
        {
            OnPause(true);
        }
    }

    public override void OnBackPressed()
    {
        OnPause(false);
    }

    protected void OnPause(bool value)
    {
        _gameManager.SetGamePaused(value);
        NavMenu.SetActive(value);
    }

    public override void OnNavItemSelected(NavItem item, object actionObj, object dataObj)
    {
        var action = (Actions)actionObj;
        var target = Transitions.Find(action);
        var data = dataObj as string[];

        OnAction(action, item, target, data);
    }

    protected abstract void OnAction(Actions action, NavItem item, NavMenu target, string[] data);
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

        Debug.Log("Not found");

        return null;
    }
}