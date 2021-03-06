﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerState
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public float MaxHealth;
    public float Health;
    public int HealthCurrentLevel;
    public float MaxStamina;
    public int StaminaCurrentLevel;
    public float Experience;
    public int SkillPoints;
    public float Credits;
    public List<SerializableAbility> Abilities;
    public List<PlayerInventory.ItemSlot> InventorySlots;

    public PlayerState()
    {
        var player = GameObject.Find("Yin");
        var playerBehaviour = player.GetComponent<PlayerBehavior>();
        var inventory = playerBehaviour.Inventory;
        position = player.transform.position;
        rotation = player.transform.rotation;

        MaxHealth = playerBehaviour.Health.MaxHealth;
        Health = playerBehaviour.Health.CurrentHealth;
        HealthCurrentLevel = playerBehaviour.Health.CurrentLevel;
        MaxStamina = playerBehaviour.Stamina.MaxStamina;
        StaminaCurrentLevel = playerBehaviour.Stamina.CurrentLevel;

        Experience = playerBehaviour.Experience.CurrentExperience;
        SkillPoints = playerBehaviour.Experience.SkillPoints;

        Credits = playerBehaviour.Currency.CurrentCredits;

        Abilities = new List<SerializableAbility>();
        foreach (var ability in player.GetComponent<AbilitiesManager>().Abilities)
        {
            Abilities.Add(ability.Serialize());
        }

        InventorySlots = inventory.Slots;
    }
}
