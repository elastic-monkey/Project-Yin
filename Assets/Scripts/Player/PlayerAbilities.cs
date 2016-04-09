using UnityEngine;
using System.Collections.Generic;

public class PlayerAbilities: MonoBehaviour {
	void Awake(){
		
	}

	public void Activate(int id){
		/*foreach (Ability ab in Abilities) {
			if (ab.Id == id) {
				ab.Active = true;
				ab.ActivateAbility ();
			} else {
				ab.Active = false;
				ab.DeactivateAbility ();
			}
		}*/
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			print ("EMPTY");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			print ("ACTIVATING SPEED");
			Activate (1);
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Activate (2);
			print ("ACTIVATING SHIELD");
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			Activate (3);
			print ("ACTIVATING STRENGTH");
		}
	}
}