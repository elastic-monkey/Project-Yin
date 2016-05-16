using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Item : MonoBehaviour
{

	public PlayerBehavior _player;

	public enum ItemType
	{
		HealthRecovery,
		StaminaRegenRate,
		Component
	}

	public Tags TagToCompare;
	[Range(0, 99)]
	public int BuyPrice;
	public int SellPrice;
	public string ItemName;

	public ItemType Type { get; protected set; }

	public abstract void UseItem();
}
