using UnityEngine;

public class MainMenuManager : MenuManager
{
    public enum Actions
    {
        New,
        Load,
        Settings,
        Quit,
        Back,
        Audio
    }

    public Actions OnBack;
    public MainMenuTransition[] Transitions;

    public override void HandleInput(bool active)
    {
        if (!active)
            return;

        if (PlayerInput.IsButtonUp(BackKey))
        {
            OnBackPressed();
        }
    }

    public void OnBackPressed()
    {
        OnNavItemSelected(null, OnBack, null);
    }

    public override void OnNavItemSelected(NavItem item, object actionObj, object dataObj)
    {
        var action = (Actions)actionObj;
        var target = Transitions.Find(action);
        var data = dataObj as string[];

        OnNavItemAction(action, item, target, data);
    }
}

[System.Serializable]
public class MainMenuTransition
{
    public string Name;
    public MainMenuManager.Actions OnAction;
    public NavMenu TargetMenu;
}

public static class MainMenuTransitionHelper
{
    public static NavMenu Find(this MainMenuTransition[] transitions, MainMenuManager.Actions action)
    {
        foreach (var transition in transitions)
        {
            if (transition.OnAction == action)
            {
                return transition.TargetMenu;
            }
        }

        //Debug.Log("Not found");

        return null;
    }
}