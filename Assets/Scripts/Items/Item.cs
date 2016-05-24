using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Item : MonoBehaviour
{
    public enum ItemType
    {
        HealthRecovery,
        StaminaRegenRate,
        Component
    }

    public Tags TagToCompare;
    public int BuyPrice;
    public int SellPrice;
    public int MaxStock;
    public Image Icon;
    public PlayerBehavior Player;
    public string ItemName;
    public string Effect;
    public string FlavorText;

    public ItemType Type { get; protected set; }

    public abstract void UseItem();

    public abstract bool CanUse();
}
