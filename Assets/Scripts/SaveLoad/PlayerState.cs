using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerState {
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
	//Something something inventory
	public List<Ability> Abilities;

	public PlayerState(){
		var player = GameObject.Find ("Player");
		var playerBehaviour = player.GetComponent<PlayerBehavior>();
		position = player.transform.position;
		rotation = player.transform.rotation;

		MaxHealth = playerBehaviour.Health.MaxHealth;
		Health = playerBehaviour.Health.CurrentHealth;
		HealthCurrentLevel = playerBehaviour.Health.CurrentLevel;
		MaxStamina = playerBehaviour.Stamina.MaxStamina;
		StaminaCurrentLevel = playerBehaviour.Stamina.CurrentLevel;

		Experience = playerBehaviour.Experience.CurrentExperience;
		SkillPoints = playerBehaviour.Experience.SkillPoints;
		Abilities = player.GetComponent<AbilitiesManager> ().Abilities;
	}
}
