using UnityEngine;
using System.Collections;
using System.IO;

public class SaveManager : MonoBehaviour {
	private int _saveSlot = 0;
	private GameState _checkpoint;

	void Start(){
		if (File.Exists ("SSS.txt")) {
			using (TextReader reader = File.OpenText ("SSS.txt")) {
				_saveSlot = int.Parse (reader.ReadLine ());
			}
			GameState state = SaveLoad.Load (_saveSlot);
			LoadPlayerState (state.PlayerState);
			LoadCameraState (state.CameraState);
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F5)) {
			SaveLoad.Save (_saveSlot);
		} else if (Input.GetKeyDown (KeyCode.F6)) {
			_checkpoint = new GameState ();
			Debug.Log ("CHECKPOINT SAVED");
		}
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
