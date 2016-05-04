using UnityEngine;
using System.Collections;

public class UpgradeMenuManager : MenuManager {

	public enum Actions
	{
		Upgrade
	}

	public NavMenu UpgradeMenu;

	private bool _isActive;
	private PlayerBehavior _player;

	private void Update(){
		if (Input.GetKeyDown (KeyCode.F1) && !_isActive) {
			ActivateUpgradeMenu (true);
		} else if (Input.GetKeyDown (KeyCode.F1) && _isActive) {
			ActivateUpgradeMenu (false);
		}
	}

	private void ActivateUpgradeMenu(bool value){
		Debug.Log ("Menu is " + value);
		_isActive = value;
		PlayerInput.GameplayBlocked = value;
		Time.timeScale = value ? 0 : 1;
		UpgradeMenu.SetActive (value);
	}

	public override void OnAction(object action, object data){
		var actionEnum = (Actions)action;

		switch (actionEnum) {
			case Actions.Upgrade:
				Debug.Log ("Upgrade not implemented");
				break;
		}
	}
}
