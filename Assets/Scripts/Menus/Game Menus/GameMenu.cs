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
        GoToBuy,
        GoToSell,
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
	private MenuSoundManager _soundManager;
	private NavMenu _navMenu;
	private bool _willOpen;

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

	protected MenuSoundManager SoundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = GameManager.MenuSoundManager;

			return _soundManager;
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

	private void LateUpdate()
	{
		if (_willOpen)
		{
			_willOpen = false;
			IsOpen = true;
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

    public virtual void Open()
    {
		SoundManager.PlayOpenSound();

		IsOpen = false;
		_willOpen = true;
		NavMenu.SetActive(true);
        Pause(true);
    }

    public virtual void Close()
    {
		IsOpen = false;
        NavMenu.SetActive(false);

		if (SubMenu)
		{
			TransitionTo(Transitions.Find(Actions.Close));
		}
		else
		{
			SoundManager.PlayCloseSound();
			Pause(false);
		}
    }

    protected void TransitionTo(GameMenu target)
    {
        if (target == null)
            return;

		if (!SubMenu)
			target.Open();

		target._willOpen = true;
		target.NavMenu.FocusCurrent();
		target.NavMenu.InputBlocked = false;

		if (target.SubMenu)
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