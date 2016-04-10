using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Slider HealthSlider;
	public float StartingHealth = 100f;
	public float CurrentHealth;
	public float DamageReduction = 50;

	private Animator _animator;

	void Awake () {
		CurrentHealth = StartingHealth;
		_animator = gameObject.GetComponent<Animator> ();
	}
	

	public void TakeDamage(float damage){
		if (CurrentHealth > 0) {
			if (_animator.GetBool(AnimatorHashIDs.DefendingBool)) {
				damage = damage * (DamageReduction * 0.01f);
			}
			CurrentHealth -= damage;
			HealthSlider.value = CurrentHealth;
		}
	}
}
