using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	public static bool CanAttack;

	public float AttackCooldown = 1.367f;
	public float AttackInactiveTime = 0.2f;
	public float AttackDamage = 20f;

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
		if (collider.CompareTag ("Enemy")) {
			print ("Enemy in Range");
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
				CanAttack = false;
				PlayerController.CanMove = false;
				_animator.SetBool (_hash.AttackingBool, true);
				_attackTimer = AttackCooldown;
				StartCoroutine (AttackCoroutine ());
				_playerStamina.ConsumeStamina (20);
			}
		}
		if (_animator.GetBool(_hash.AttackingBool)) {
			if (_attackTimer > 0) {
				_attackTimer -= Time.deltaTime;
			} else {
				_animator.SetBool (_hash.AttackingBool, false);
				PlayerController.CanMove = true;
				CanAttack = true;
			}
		}
	}

	IEnumerator AttackCoroutine(){
		print ("routine");
		yield return new WaitForSeconds (AttackInactiveTime);
		foreach(Collider enemy in _enemies){
			float dot = Vector3.Dot(transform.forward, (enemy.transform.position - transform.position).normalized);
			if(dot > 0.7f){ 
				enemy.GetComponent<EnemyHealthStamina> ().TakeDamage (AttackDamage);
			}
		}
	}
}
