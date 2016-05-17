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
    public string ItemName;

    public ItemType Type { get; protected set; }

    public abstract void UseItem(PlayerBehavior player);
}
