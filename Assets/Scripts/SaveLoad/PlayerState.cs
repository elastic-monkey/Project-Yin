using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerState {
	public SerializableVector3 position;
	public SerializableQuaternion rotation;
	public float Health;
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
		Health = playerBehaviour.Health.CurrentHealth;
		Experience = playerBehaviour.Experience.CurrentExperience;
		SkillPoints = playerBehaviour.Experience.SkillPoints;
		Abilities = player.GetComponent<AbilitiesManager> ().Abilities;
	}
}
