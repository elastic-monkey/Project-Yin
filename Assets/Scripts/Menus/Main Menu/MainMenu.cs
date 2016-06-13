using UnityEngine;

public class MainMenu : MonoBehaviour, IMenu
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

    public bool SubMenu = false;
    public bool Current = false;
    public MainMenuTransition[] Transitions;

	private MenuSoundManager _soundManager;
    private NavMenu _navMenu;

	public MenuSoundManager SoundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = MainMenuManager.Instance.SoundManager;

			return _soundManager;
		}
	}

    public NavMenu NavMenu
    {
        get
        {
            if (_navMenu == null)
                _navMenu = GetComponent<NavMenu>();

            return _navMenu;
        }
    }

    private void Update()
    {
        if (!Current)
            return;

        if (PlayerInput.IsButtonUp(Axes.Back))
        {
            TransitionTo(Transitions.Find(Actions.Back));
        }
    }

    public virtual void OnNavItemFocused(NavItem target)
    {
		if (!NavMenu.IsActive)
			return;

		SoundManager.PlayFocusItemSound();
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

    protected void TransitionTo(MainMenu other)
    {
        if (other == null)
            return;

		SoundManager.PlayOpenSound();

        other.NavMenu.SetActive(true);
        other.Current = true;

        if (other.SubMenu)
        {
            NavMenu.InputBlocked = true;
            NavMenu.UnfocusAll();
            Current = false;
        }
        else
        {
            NavMenu.SetActive(false);
            Current = false;
        }
    }

    private void OnValidate()
    {
        if (Transitions.Length > 0)
            foreach (var t in Transitions)
                t.Name = t.OnAction.ToString();
    }
}

[System.Serializable]
public class MainMenuTransition
{
    [HideInInspector]
    public string Name;
    public MainMenu.Actions OnAction;
    public MainMenu TargetMenu;
}

public static class MainMenuTransitionHelper
{
    public static MainMenu Find(this MainMenuTransition[] transitions, MainMenu.Actions action)
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