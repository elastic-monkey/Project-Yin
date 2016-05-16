using UnityEngine;
using System.Collections;

[System.Serializable]
public class StaminaSyringe : Item
{

	public float StaminaRegenMulti;
	public float Duration;

	public StaminaSyringe()
	{
		Type = ItemType.StaminaRegenRate;
	}

	public override void UseItem()
	{
		StartCoroutine(UseStaminaSyringe());
	}

	private IEnumerator UseStaminaSyringe()
	{
		_player.Stamina.RegenerationRate *= StaminaRegenMulti;

		yield return new WaitForSeconds(Duration);

		_player.Stamina.RegenerationRate /= StaminaRegenMulti;
	}
}
