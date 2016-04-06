using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthStamina : MonoBehaviour {

	public float StartingHealth = 100f;
	public float StartingStamina = 100f;
	public float CurrentHealth;
	public float CurrentStamina;
	public float StaminaRegenRate = 10f;
	public Slider HealthSlider;

	void Awake(){
		CurrentHealth = StartingHealth;
		CurrentStamina = StartingStamina;
	}

	void Update(){
		if (CurrentStamina < StartingStamina) {
			ConsumeStamina (-StaminaRegenRate*Time.deltaTime);
		}
	}

	public void TakeDamage(float damage){
		if (CurrentHealth > 0) {
			CurrentHealth -= damage;
			HealthSlider.value = CurrentHealth;
		}
	}

	public void ConsumeStamina(float stamina){
		if (CanConsume(stamina)) {
			CurrentStamina -= stamina;
		}
	}

	public bool CanConsume(float stamina){
		return CurrentStamina > stamina;
	}
}
