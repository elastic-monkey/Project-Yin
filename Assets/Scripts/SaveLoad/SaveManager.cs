using UnityEngine;
using System.Collections;
using System.IO;

public class SaveManager : MonoBehaviour
{
	public static bool LoadCheckpoint;

    void Start()
    {
        if (File.Exists("SSS.txt"))
        {
            GameState state = SaveLoad.Load(false);
            LoadAllStates(state);
        }
    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.F5)) {
			SaveLoad.Save (false);
		} else if (Input.GetKeyDown (KeyCode.F6)) {
			SaveLoad.Save (true);
		} else if (Input.GetKeyDown (KeyCode.F8)) {
			LoadAllStates (SaveLoad.Load (true));
		} else if (LoadCheckpoint) {
			LoadCheckpoint = false;
			LoadAllStates (SaveLoad.Load (true));
		}
    }

    private void LoadAllStates(GameState state)
    {
        LoadPlayerState(state.PlayerState);
        LoadCameraState(state.CameraState);
    }

    private void LoadPlayerState(PlayerState state)
    {
        var player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        player.transform.position = state.position;
        player.transform.rotation = state.rotation;
        player.Health.CurrentHealth = state.Health;
    }

    private void LoadCameraState(CameraState state)
    {
        GameObject camera = GameObject.Find("Camera Pivot");
        camera.transform.position = state.position;
    }
}
