using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	public static bool CanAttack;

	public float AttackCooldown = 1f;
	public float AttackInactiveTime = 1f;
	public float AttackDamage = 20f;

	private List<Collider> _enemies;
	private bool _attacking;
	private float _attackTimer;
	private Animator _animator;
	private PlayerHealthStamina _playerStats;
	private HashIDs _hash;

	void Awake(){
		_enemies = new List<Collider> ();
		_playerStats = gameObject.GetComponentInParent<PlayerHealthStamina> ();
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
				_attacking = true;
				_attackTimer = AttackCooldown;
				_animator.SetBool (_hash.AttackingBool, true);
				StartCoroutine (AttackCoroutine ());
				_playerStats.ConsumeStamina (20);
			}

			if (_attacking) {
				if (_attackTimer > 0) {
					_attackTimer -= Time.deltaTime;
				} else {
					_attacking = false;
					_animator.SetBool (_hash.AttackingBool, false);
				}
			}
		}
	}

	IEnumerator AttackCoroutine(){
		print ("ATTACK1");
		yield return new WaitForSeconds (AttackInactiveTime);
		print ("ATTACK2");
		foreach(Collider enemy in _enemies){
			float dot = Vector3.Dot(transform.forward, (enemy.transform.position - transform.position).normalized);
			if(dot > 0.7f){ 
				enemy.GetComponent<EnemyHealthStamina> ().TakeDamage (AttackDamage);
			}
		}
	}
}
