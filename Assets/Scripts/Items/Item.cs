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
        Component,
        Null
    }

    public Tags TagToCompare;
    public ItemType Type;
    public int BuyPrice;
    public int SellPrice;
    public int MaxStock;
    public Image Icon;
    public string ItemName;
    [TextArea(1, 4)]
    public string Effect;
    [TextArea(1, 4)]
    public string FlavorText;

    [SerializeField]
    protected PlayerBehavior Player;

    private void Awake()
    {
        Player = GameManager.Instance.Player;
    }

    public abstract void UseItem();

    public abstract bool CanUse();
}
