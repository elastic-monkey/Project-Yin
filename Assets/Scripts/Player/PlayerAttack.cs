using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	public static bool CanAttack;
	public static float StaminaMulti = 1.0f;
	public static float DamageMulti = 1.0f;

	public float AttackCooldown = 1.2f;
	public float AttackInactiveTime = 0.2f;
	public float AttackDamage = 20f;
	public float StaminaCost = 20f;

	private List<Collider> _enemies;
	private float _attackTimer;
	private Animator _animator;
	private PlayerStamina _playerStamina;
	private HashIDs _hash;

	void Awake(){
		_enemies = new List<Collider> ();
		_playerStamina = gameObject.GetComponentInParent<PlayerStamina> ();
		_animator = gameObject.GetComponentInParent<Animator> ();
		_hash = gameObject.GetComponentInParent<HashIDs> ();
		CanAttack = true;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag ("Enemy") && !_enemies.Contains(collider)) {
			_enemies.Add (collider);
		}
	}
		
	void OnTriggerExit(Collider collider)
	{
		_enemies.Remove (collider);
	}

	void Update(){
		AttackManagement ();
	}

	void AttackManagement(){
		if (CanAttack) {
			if (Input.GetButtonDown ("Fire2")) {
				StartCoroutine (AttackCoroutine ());
			}
		}
	}

	IEnumerator AttackCoroutine(){
		CanAttack = false;
		PlayerController.CanMove = false;
		_animator.SetBool (_hash.AttackingBool, true);
		_playerStamina.ConsumeStamina (StaminaCost * StaminaMulti);

		yield return new WaitForSeconds (AttackInactiveTime);
		foreach(Collider enemy in _enemies){
			float dot = Vector3.Dot(transform.forward, (enemy.transform.position - transform.position).normalized);
			if(dot > 0.7f){ 
				enemy.GetComponent<EnemyHealthStamina> ().TakeDamage (AttackDamage * DamageMulti);
			}
		}
		yield return new WaitForSeconds(Mathf.Max(0, AttackCooldown - AttackInactiveTime));

		_animator.SetBool (_hash.AttackingBool, false);
		PlayerController.CanMove = true;
		CanAttack = true;
	}
}
