using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenuManager : MenuManager
{

    public enum Actions
    {
        UpgradeHealth,
        UpgradeStamina,
        UpgradeSpeed,
        UpgradeShield,
        UpgradeStrength,
        CloseMenu
    }

    public NavMenu UpgradeMenu;
    public Text AvailableSP;
    public Text UpgradeCost;
    public Text EffectText;

    private UpgradeMenuNavItem[] _items;
    private GameManager _gameManager;

    public GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;

            return _gameManager;
        }
    }

    public PlayerBehavior Player
    {
        get
        {
            return GameManager.Player;
        }
    }

    public void Start()
    {
        _items = UpgradeMenu.GetComponentsInChildren<UpgradeMenuNavItem>();
        UpdateAllItems();
    }

    private void Update()
    {
        //TODO Change to proper keys
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_gameManager.GamePaused)
            {
                if (UpgradeMenu.IsActive)
                {
                    OnUpgradeMenu(false);
                }
            }
            else
            {
                OnUpgradeMenu(true);
            }
        }
    }

    public override void OnFocus(NavItem target)
    {
        var upgradeItem = target as UpgradeMenuNavItem;
        if (upgradeItem == null)
            return;

        var action = upgradeItem.Action;
        switch (action)
        {
            case Actions.UpgradeHealth:
                OnItemFocused(upgradeItem, Player.Health);
                break;

            case Actions.UpgradeStamina:
                OnItemFocused(upgradeItem, Player.Stamina);
                break;

            case Actions.UpgradeSpeed:
                var upgradable = Player.Abilities.Find(Ability.AbilityType.Speed);
                if (upgradable != null)
                    OnItemFocused(upgradeItem, upgradable);
                break;

            case Actions.UpgradeShield:
                upgradable = Player.Abilities.Find(Ability.AbilityType.Shield);
                if (upgradable != null)
                    OnItemFocused(upgradeItem, upgradable);
                break;

            case Actions.UpgradeStrength:
                upgradable = Player.Abilities.Find(Ability.AbilityType.Strength);
                if (upgradable != null)
                    OnItemFocused(upgradeItem, upgradable);
                break;
        }
    }

    private void OnItemFocused(UpgradeMenuNavItem navItem, Upgradable upgradable)
    {
        UpgradeCost.text = upgradable.UpgradeCost(navItem.UpgradeLevel).ToString();
        UpdateAvailableSP();
    }

    private void UpdateAllItems()
    {
        foreach (var item in _items)
        {
            switch (item.Action)
            {
                case Actions.UpgradeHealth:
                    PurchaseOrDisable(item, Player.Health);
                    break;

                case Actions.UpgradeStamina:
                    PurchaseOrDisable(item, Player.Stamina);
                    break;

                case Actions.UpgradeSpeed:
                    PurchaseOrDisable(item, Player.Abilities.Find(Ability.AbilityType.Speed));
                    break;

                case Actions.UpgradeShield:
                    PurchaseOrDisable(item, Player.Abilities.Find(Ability.AbilityType.Shield));
                    break;

                case Actions.UpgradeStrength:
                    PurchaseOrDisable(item, Player.Abilities.Find(Ability.AbilityType.Strength));
                    break;
            }
        }
    }

    private void PurchaseOrDisable(UpgradeMenuNavItem item, Upgradable upgradable)
    {
        var currentLevel = upgradable.CurrentLevel;
        if (currentLevel >= item.UpgradeLevel)
        {
            item.SetPurchased(true);
        }
        else if (currentLevel == item.UpgradeLevel - 1)
        {
            item.SetDisabled(!upgradable.CanBeUpgradedTo(item.UpgradeLevel, Player));
        }
        else
        {
            item.SetDisabled(true);
        }
    }

    private void OnUpgradeMenu(bool value)
    {
        _gameManager.SetGamePaused(value);

        UpdateAvailableSP();
        UpgradeMenu.SetActive(value);
    }

    public override void OnAction(NavItem item, object action, object data)
    {
        var upgradeItem = item as UpgradeMenuNavItem;
        if (upgradeItem == null)
            return;
        
        var actionEnum = upgradeItem.Action;
        var level = (int)data; // put in range [0-3]

        switch (actionEnum)
        {
            case Actions.UpgradeHealth:
                if (Player.Health.UpgradeTo(level, Player))
                {
                    upgradeItem.SetPurchased(true);
                    OnItemFocused(upgradeItem, Player.Health);
                }
                break;

            case Actions.UpgradeStamina:
                if (Player.Stamina.UpgradeTo(level, Player))
                {
                    upgradeItem.SetPurchased(true);
                    OnItemFocused(upgradeItem, Player.Stamina);
                }
                break;

            case Actions.UpgradeSpeed:
                var type = Ability.AbilityType.Speed;
                if (Player.Abilities.UpgradeAbility(type, level))
                {
                    upgradeItem.SetPurchased(true);
                    OnItemFocused(upgradeItem, Player.Abilities.Find(type));
                }
                break;

            case Actions.UpgradeShield:
                type = Ability.AbilityType.Shield;
                if (Player.Abilities.UpgradeAbility(type, level))
                {
                    upgradeItem.SetPurchased(true);
                    OnItemFocused(upgradeItem, Player.Abilities.Find(type)); 
                }
                break;

            case Actions.UpgradeStrength:
                type = Ability.AbilityType.Strength;
                if (Player.Abilities.UpgradeAbility(type, level))
                {
                    upgradeItem.SetPurchased(true);
                    OnItemFocused(upgradeItem, Player.Abilities.Find(type)); 
                }
                break;

            case Actions.CloseMenu:
                OnUpgradeMenu(false);
                break;
        }

        UpdateAllItems();
    }

    private void UpdateAvailableSP()
    {
        AvailableSP.text = Player.Experience.SkillPoints.ToString();
    }
}
