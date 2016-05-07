using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerBehavior))]
public class AbilitiesManager : MonoBehaviour
{
    public Transform AbilitesTransform;
    public List<Ability> Abilities;

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
                    ability.SetActive(_player);
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
    }

    public void RemoveAbilities()
    {
        for (var i = 0; i < AbilitesTransform.childCount; i++)
        {
            Destroy(AbilitesTransform.GetChild(i).gameObject);
        }
        Abilities.Clear();
    }

    public void UpgradeAbility(Ability.AbilityType type, int level)
    {
        for (int i = 0; i < Abilities.Count; i++)
        {
            var ability = Abilities[i];

            if (ability.Type.Equals(type) && ability.CanBeUpgradedTo(level))
            {
                var cost = ability.UpgradeCost(level);
                if (_player.Experience.SkillPoints >= cost)
                {
                    _player.Experience.ConsumeSkillPoints(cost);
                    ability.UpgradeTo(level);
                }
            }
        }
    }
}