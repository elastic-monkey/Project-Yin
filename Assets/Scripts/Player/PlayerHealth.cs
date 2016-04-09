using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Slider HealthSlider;
	public float StartingHealth = 100f;
	public float CurrentHealth;
	public float DamageReduction = 50;

	private Animator _animator;
	private HashIDs _hash;

	void Awake () {
		CurrentHealth = StartingHealth;
		_animator = gameObject.GetComponent<Animator> ();
		_hash = gameObject.GetComponent<HashIDs> ();
	}
	

	public void TakeDamage(float damage){
		if (CurrentHealth > 0) {
			if (_animator.GetBool(_hash.DefendingBool)) {
				damage = damage * (DamageReduction / 100.0f);
			}
			CurrentHealth -= damage;
			HealthSlider.value = CurrentHealth;
		}
	}
}
