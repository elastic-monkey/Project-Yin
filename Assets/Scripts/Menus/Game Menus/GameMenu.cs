using UnityEngine;
using System.Collections;

public class GameMenu : Menu
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

    public bool PausesGame = true;
    public bool BlockGameplayInput = true;
    public GameMenuTransition[] Transitions;
    private GameManager _gameManager;

    protected GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;

            return _gameManager;
        }
    }

    public override void Open()
    {
        base.Open();

        Pause(true);
    }

    public override void Close()
    {
        base.Close();

        if (NavMenu.IsSubMenu)
        {
            TransitionTo(Transitions.Find(Actions.Close));
        }
        else
        {
            Pause(false);
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

    protected override NavMenu FindTransitionTarget(object actionObj)
    {
        return Transitions.Find((Actions)actionObj);
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
        public NavMenu TargetMenu;
    }
}

public static class GameMenuTransitionHelper
{
    public static NavMenu Find(this GameMenu.GameMenuTransition[] transitions, GameMenu.Actions action)
    {
        foreach (var transition in transitions)
            if (transition.OnAction == action)
                return transition.TargetMenu;

        return null;
    }
}