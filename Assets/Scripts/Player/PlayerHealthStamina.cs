using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthStamina : MonoBehaviour {

	public float StartingHealth = 100f;
	public float StartingStamina = 100f;
	public float CurrentHealth;
	public float CurrentStamina;
	public float StaminaRegenRate = 10f;
	public Slider HealthSlider;
	public Slider StaminaSlider;
	public float DamageReduction = 50;

	private Animator _animator;
	private HashIDs _hash;

	void Awake(){
		CurrentHealth = StartingHealth;
		CurrentStamina = StartingStamina;
		_animator = gameObject.GetComponent<Animator> ();
		_hash = gameObject.GetComponent<HashIDs> ();
	}

	void Update(){
		if (CurrentStamina < StartingStamina) {
			ConsumeStamina (-StaminaRegenRate*Time.deltaTime);
		}
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

	public void ConsumeStamina(float stamina){
		if (CanConsume(stamina)) {
			CurrentStamina -= stamina;
			StaminaSlider.value = CurrentStamina;
		}
	}

	public bool CanConsume(float stamina){
		return CurrentStamina > stamina;
	}
}
