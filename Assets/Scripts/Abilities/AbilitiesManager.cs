using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerBehavior))]
public class AbilitiesManager : MonoBehaviour
{
    public Transform AbilitesTransform;
    public List<Ability> Abilities;
    public List<Image> AbilityHUDIcons;
    public List<Sprite> AbilityActiveSprite;
    public List<Sprite> AbilityInactiveSprite;

    private PlayerBehavior _player;

    void Awake()
    {
        _player = GetComponent<PlayerBehavior>();
    }

    public void ApplyAbilities()
    {
        var selectedAbility = -1;
        for (var i = 0; i < Abilities.Count; i++)
        {
            if (PlayerInput.IsButtonDown(Abilities[i].InputAxis))
            {
                selectedAbility = i;
            }
        }
        if (selectedAbility != -1)
        {
            for (var i = 0; i < Abilities.Count; i++)
            {
                var ability = Abilities[i];
                if (i == selectedAbility)
                {
                    if (!ability.Active)
                    {
                        ability.SetActive(_player);
                    }
                    else
                    {
                        ability.Deactivate(_player);
                    }
                }
                else
                {
                    ability.Deactivate(_player);
                }
            }
        }
    }

    public void Add(Ability ability)
    {
        ability.transform.SetParent(AbilitesTransform);
        Abilities.Add(ability);
        ability.HUDIcon = AbilityHUDIcons[GetAbilityIndex(ability.Type())];
        ability.ActiveIcon = AbilityActiveSprite[GetAbilityIndex(ability.Type())];
        ability.DeactivatedIcon = AbilityInactiveSprite[GetAbilityIndex(ability.Type())];
    }

    public void RemoveAbilities()
    {
        for (var i = 0; i < AbilitesTransform.childCount; i++)
        {
            Destroy(AbilitesTransform.GetChild(i).gameObject);
        }
        Abilities.Clear();
    }

    public Ability Find(Ability.AbilityType type)
    {
        foreach (var ability in Abilities)
        {
            if (ability.Type().Equals(type))
                return ability;
        }

        return null;
    }

    public bool UpgradeAbility(Ability.AbilityType type, int level)
    {
        foreach (var ability in Abilities)
        {
            if (ability.Type().Equals(type))
            {
                return ability.UpgradeTo(level, _player);
            }
        }

        return false;
    }

    private int GetAbilityIndex(Ability.AbilityType type)
    {
        if (type == Ability.AbilityType.Speed)
        {
            return 0;
        }
        else if (type == Ability.AbilityType.Shield)
        {
            return 1;
        }
        else if (type == Ability.AbilityType.Strength)
        {
            return 2;
        }

        return 0;
    }
}