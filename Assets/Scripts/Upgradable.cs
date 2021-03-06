﻿using UnityEngine;
using System.Collections;

public abstract class Upgradable : MonoBehaviour
{
    public enum UpgradableTypes
    {
        Health,
        Stamina,
        Speed,
        Shield,
        Strength
    }

    public const int MaxLevel = 4;

    public Color DefaultColor, ActiveColor;
    public Sprite DefaultSprite, ActiveSprite;
    public int CurrentLevel;

    public int UpgradeCost(int level)
    {
        return (int)Mathf.Ceil(level * 0.5f);
    }

    public bool CanBeUpgradedTo(int level, PlayerBehavior player)
    {
        if (level < 0 && level > MaxLevel)
            return false;

        if (level != CurrentLevel + 1)
            return false;

        if (player.Experience.SkillPoints < UpgradeCost(level))
            return false;
        
        return OnCanBeUpgradedTo(level);
    }

    public bool UpgradeTo(int level, PlayerBehavior player)
    {
        if(!CanBeUpgradedTo(level, player))
            return false;

        player.Experience.ConsumeSkillPoints(UpgradeCost(level));
        CurrentLevel = level;

        OnUpgradeTo(level);

        return true;
    }

    protected abstract void OnUpgradeTo(int level);

    protected abstract bool OnCanBeUpgradedTo(int level);

    public abstract string GetFlavorText();

    public abstract string GetEffectText(int level);
}
