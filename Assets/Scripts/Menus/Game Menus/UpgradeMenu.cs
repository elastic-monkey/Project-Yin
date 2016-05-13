using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenu : GameMenuManager
{
    public Text AvailableSP, UpgradeCost, EffectText;

    private UpgradeMenuNavItem[] _items;

    public PlayerBehavior Player
    {
        get
        {
            return _gameManager.Player;
        }
    }

    public void Start()
    {
        _items = NavMenu.GetComponentsInChildren<UpgradeMenuNavItem>();
        UpdateAllItems();
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
        
    private void UpdateAvailableSP()
    {
        AvailableSP.text = Player.Experience.SkillPoints.ToString();
    }

    protected override void OnAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        var action = (Actions)actionObj;

        var upgradeItem = item as UpgradeMenuNavItem;
        var level = int.Parse(data[0]); // put in range [0-3]

        switch (action)
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

            case Actions.Back:
                UpdateAvailableSP();
                OnPause(false);
                break;
        }

        UpdateAllItems();
    }
}