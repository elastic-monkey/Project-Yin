using UnityEngine;
using System.Collections.Generic;

public class AbilityManagement : MonoBehaviour {

	private PlayerMovement _playerMovement;
	private AttackBehavior _attack;
	private DefenseBehavior _defense;
	private List<Ability> _abilities; 

	void Awake(){
		_playerMovement = gameObject.GetComponent<PlayerMovement> ();
		_attack = gameObject.GetComponent<AttackBehavior> ();
		_defense = gameObject.GetComponent<DefenseBehavior> ();
		LoadAbilities ();
	}

	public void ActivateAbilities(PlayerInput input){
		if (input.VisionMode) {

		} else if (input.SpeedMode) {
			print ("SpeedMode ON");
			ActivateSpeedMode (true);
			ActivateStrengthMode (false);
		} else if (input.ShieldMode) {
			print ("ShieldMode ON");
			ActivateShieldMode (true);
		} else if (input.StrengthMode) {
			print ("StrengthMode ON");
			ActivateStrengthMode (true);
			ActivateSpeedMode (false);
		}
	}

	private void ActivateSpeedMode(bool active){
		float speedMulti = 1.0f;
		if (active) {
			speedMulti = 1.0f + 0.05f * _abilities [1].Level;
		}
		_playerMovement.MoveSpeedMulti = speedMulti;
	}

	private void ActivateStrengthMode(bool active){
		float multi = 1.0f;
		if (active) {
			multi = 2f;
		} 
		_attack.DamageMultiplier = multi;
		_attack.StaminaMultiplier = multi;
	}

	private void ActivateShieldMode(bool active){
		_defense.ShieldOn = true;
	}

	private void LoadAbilities(){
		var numAbilities = 4;
		_abilities = new List<Ability> ();

		for (int i = 0; i < numAbilities; i++) {
			Ability abil = new Ability ();
			abil.Id = i;
			//abil.Level = AbilityReader.GetAbilityLevel (i);
			abil.Level = 10;
			_abilities.Add(abil);
		}
	}
}

public class Ability{
	private const int MaxLevel = 4;

	public int Id;
	public int Level;

	public bool CanUpgrade{
		get{
			return Level < MaxLevel;
		}
	}

}