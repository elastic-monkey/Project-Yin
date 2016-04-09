using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour {

	public Slider StaminaSlider;
	public float StartingStamina = 100f;
	public float CurrentStamina;
	public float StaminaRegenRate = 10f;

	// Use this for initialization
	void Awake () {
		CurrentStamina = StartingStamina;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentStamina < StartingStamina) {
			ConsumeStamina (-StaminaRegenRate*Time.deltaTime);
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
