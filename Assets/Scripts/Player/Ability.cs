using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {

	public int Id;
	public string Description;
	public bool Active;
	public int Level;

	abstract public void ActivateAbility ();
	abstract public void DeactivateAbility();
}

public class SpeedMode : Ability {

	private Animator _animator;
	private float SpeedModifier = 1.2f;

	public SpeedMode(){
		this.Id = 1;
		this.Description = "Speed Mode";
		this.Active = false;
		_animator = gameObject.GetComponent<Animator> ();
	}

	public override void ActivateAbility(){
		print ("SPEED MODE ACTIVATED");
	}

	public override void DeactivateAbility(){

	}

	void FixedUpdate(){
		if( Active && _animator.GetCurrentAnimatorStateInfo(-1).IsName("MOVEMENT"))
		{
			_animator.speed = SpeedModifier;
		}
	}
}

public class ShieldMode : Ability {

	public ShieldMode(){
		this.Id = 2;
		this.Description = "Speed Mode";
		this.Active = false;
	}

	public override void ActivateAbility(){

	}

	public override void DeactivateAbility(){

	}
}

public class StrengthMode : Ability {

	public StrengthMode(){
		this.Id = 3;
		this.Description = "Speed Mode";
		this.Active = false;
	}

	public override void ActivateAbility(){

	}

	public override void DeactivateAbility(){

	}
}
