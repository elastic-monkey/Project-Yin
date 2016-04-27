using UnityEngine;
using System.Collections;
using System.IO;

public class SaveManager : MonoBehaviour {
	private GameState _checkpoint;

	void Start(){
		if (File.Exists ("SSS.txt")) {
			GameState state = SaveLoad.Load ();
			LoadAllStates (state);
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F5)) {
			SaveLoad.Save ();
		} else if (Input.GetKeyDown (KeyCode.F6)) {
			_checkpoint = new GameState ();
		} else if (Input.GetKeyDown (KeyCode.F8)) {
			LoadAllStates (_checkpoint);
		}
	}


	private void LoadAllStates(GameState state){
		LoadPlayerState (state.PlayerState);
		LoadCameraState (state.CameraState);
	}

	private void LoadPlayerState(PlayerState state){
		GameObject player = GameObject.Find("Player");
		player.transform.position = state.position;
		player.transform.rotation = state.rotation;
		player.GetComponent<Health>().CurrentHealth = state.Health;
	}

	private void LoadCameraState(CameraState state){
		GameObject camera = GameObject.Find ("Camera Pivot");
		camera.transform.position = state.position;
	}
}
