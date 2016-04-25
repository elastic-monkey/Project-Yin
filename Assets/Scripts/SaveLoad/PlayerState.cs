using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerState {
	public SerializableVector3 position;
	public SerializableQuaternion rotation;
	public float Health;
	public float Experience;
	public int SkillPoints;
	public float Credits;
	//Something something inventory
	//Something something abilities

	public PlayerState(){
		GameObject player = GameObject.Find ("Player");
		position = player.transform.position;
		rotation = player.transform.rotation;
		Health = player.GetComponent<Health> ().CurrentHealth;
		Experience = player.GetComponent<Experience> ().CurrentExp;
		SkillPoints = player.GetComponent<Experience> ().SkillPoints;
	}
}
