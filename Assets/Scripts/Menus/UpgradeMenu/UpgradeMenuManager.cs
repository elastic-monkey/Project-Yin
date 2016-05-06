using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenuManager : MenuManager {

	public enum Actions
	{
		UpgradeHealth,
		UpgradeStamina,
		UpgradeSpeed,
		UpgradeShield,
		UpgradeStrenght
	}

	public NavMenu UpgradeMenu;

	private bool _isActive;
	private PlayerBehavior _player;
	private Text _availableSP;

	private void Start(){
		_player = GameObject.Find ("Player").GetComponent<PlayerBehavior> ();
		_availableSP = GameObject.Find("AvailableSP").GetComponent<Text>();
	}

	private void Update(){
		//TODO Change to proper keys
		if (Input.GetKeyDown (KeyCode.F1) && !_isActive) {
			ActivateUpgradeMenu (true);
		} else if (Input.GetKeyDown (KeyCode.F1) && _isActive) {
			ActivateUpgradeMenu (false);
		}
	}

	private void ActivateUpgradeMenu(bool value){
		_isActive = value;
		PlayerInput.GameplayBlocked = value;
		Time.timeScale = value ? 0 : 1;
		UpdateAvailableSP ();
		UpgradeMenu.SetActive (value);
	}

	public override void OnAction(object action, object data){
		var actionEnum = (Actions)action;

		switch (actionEnum) {
			case Actions.UpgradeHealth:
				Debug.Log ("Upgrading Health");
				UpgradeHealth ();
				break;
			case Actions.UpgradeStamina:
				Debug.Log ("Upgrading Stamina");
				UpgradeStamina ();
				break;
			case Actions.UpgradeSpeed:
				Debug.Log ("Upgrading Speed");
				UpgradeAbility (Ability.Type.Speed);
				break;
			case Actions.UpgradeShield:
				Debug.Log ("Upgrading Shield");
				UpgradeAbility (Ability.Type.Shield);
				break;
			case Actions.UpgradeStrenght:
				Debug.Log("Upgrading Strenght");
				UpgradeAbility (Ability.Type.Strength);
				break;
		}
	}

	private void UpgradeHealth(){
		_player.Health.Upgrade();
		UpdateAvailableSP ();
	}

	private void UpgradeStamina(){
		_player.Stamina.Upgrade ();
		UpdateAvailableSP ();
	}

	private void UpgradeAbility(Ability.Type type){
		_player.Abilities.UpgradeAbility (type);
		UpdateAvailableSP ();
	}

	private void UpdateAvailableSP(){
		_availableSP.text = "Skill Points: " + _player.Experience.SkillPoints;
	}
}
