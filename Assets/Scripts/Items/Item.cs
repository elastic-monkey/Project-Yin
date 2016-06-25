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
    public Sprite Icon;
    public string ItemName;
    [TextArea(1, 4)]
    public string Effect;
    [TextArea(1, 4)]
    public string FlavorText;

    [SerializeField]
    protected PlayerBehavior _player;

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    public abstract void UseItem();

    public abstract bool CanUse();
}
