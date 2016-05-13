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

    public override void OnNavItemFocused(NavItem target)
    {
        base.OnNavItemFocused(target);

        NavMenu.UseHoverNavigation = false; // <-- default

        var upgradeItem = target as UpgradeMenuNavItem;
        if (upgradeItem == null)
            return;

        NavMenu.UseHoverNavigation = true; // <-- use only for upgrade items

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
                OnItemFocused(upgradeItem, Player.Abilities.Find(Ability.AbilityType.Speed));
                break;

            case Actions.UpgradeShield:
                OnItemFocused(upgradeItem, Player.Abilities.Find(Ability.AbilityType.Shield));
                break;

            case Actions.UpgradeStrength:
                OnItemFocused(upgradeItem, Player.Abilities.Find(Ability.AbilityType.Strength));
                break;
        }
    }

    private void OnItemFocused(UpgradeMenuNavItem navItem, Upgradable upgradable)
    {
        if (upgradable == null)
            return;
        
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
            item.Purchase(true);
        }
        else if (currentLevel == item.UpgradeLevel - 1)
        {
            item.Disable(!upgradable.CanBeUpgradedTo(item.UpgradeLevel, Player));
        }
        else
        {
            item.Disable(true);
        }
    }

    private void UpdateAvailableSP()
    {
        AvailableSP.text = Player.Experience.SkillPoints.ToString();
    }

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        base.OnNavItemAction(actionObj, item, target, data);

        var action = (Actions)actionObj;
        if (action < Actions.UpgradeHealth || action > Actions.UpgradeStrength)
            return;

        var upgradeItem = item as UpgradeMenuNavItem;
        var level = (item != null) ? int.Parse(data[0]) : -1;

        switch (action)
        {
            case Actions.UpgradeHealth:
                if (Player.Health.UpgradeTo(level, Player))
                {
                    upgradeItem.Purchase(true);
                    OnItemFocused(upgradeItem, Player.Health);
                }
                break;

            case Actions.UpgradeStamina:
                if (Player.Stamina.UpgradeTo(level, Player))
                {
                    upgradeItem.Purchase(true);
                    OnItemFocused(upgradeItem, Player.Stamina);
                }
                break;

            case Actions.UpgradeSpeed:
                var type = Ability.AbilityType.Speed;
                if (Player.Abilities.UpgradeAbility(type, level))
                {
                    upgradeItem.Purchase(true);
                    OnItemFocused(upgradeItem, Player.Abilities.Find(type));
                }
                break;

            case Actions.UpgradeShield:
                type = Ability.AbilityType.Shield;
                if (Player.Abilities.UpgradeAbility(type, level))
                {
                    upgradeItem.Purchase(true);
                    OnItemFocused(upgradeItem, Player.Abilities.Find(type)); 
                }
                break;

            case Actions.UpgradeStrength:
                type = Ability.AbilityType.Strength;
                if (Player.Abilities.UpgradeAbility(type, level))
                {
                    upgradeItem.Purchase(true);
                    OnItemFocused(upgradeItem, Player.Abilities.Find(type)); 
                }
                break;
        }

        UpdateAllItems();
    }
}