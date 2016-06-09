using UnityEngine;

public class MainMenuManager : Menu
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

    protected override NavMenu FindTransitionTarget(object actionObj)
    {
        return Transitions.Find((Actions)actionObj);
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

        return null;
    }
}