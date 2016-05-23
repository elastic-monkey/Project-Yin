﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public Slider Health, Stamina, Experience;
    public Image AvailableSkillPoints;

    private PlayerBehavior _player;

    private void Start()
    {
        _player = GameManager.Instance.Player;

        Health.minValue = 0;
        Health.value = _player.Health.CurrentHealth;
        Health.maxValue = _player.Health.MaxHealth;
        Health.wholeNumbers = false;

        Stamina.minValue = 0;
        Stamina.value = _player.Stamina.CurrentStamina;
        Stamina.value = _player.Stamina.MaxStamina;
        Stamina.wholeNumbers = false;

        Experience.minValue = 0;
        Experience.value = _player.Experience.CurrentExperience;
        Experience.maxValue = _player.Experience.MaxExperience;
        Experience.wholeNumbers = false;

        AvailableSkillPoints.enabled = _player.Experience.HasSkillPoints;
    }
}