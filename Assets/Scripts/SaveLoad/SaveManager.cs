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
			Debug.Log ("SSS present. Loading savegame");
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
		var player = GameObject.Find ("Player");
		var playerBehavior = player.GetComponent<PlayerBehavior>();
        player.transform.position = state.position;
        player.transform.rotation = state.rotation;

		playerBehavior.Health.MaxHealth = state.MaxHealth;
		playerBehavior.Health.CurrentHealth = state.Health;
		playerBehavior.Health.CurrentLevel = state.HealthCurrentLevel;
		playerBehavior.Stamina.MaxStamina = state.MaxStamina;
		playerBehavior.Stamina.CurrentLevel = state.StaminaCurrentLevel;

		playerBehavior.Experience.CurrentExperience = state.Experience;
		playerBehavior.Experience.SkillPoints = state.SkillPoints;
		var playerAbilities = player.GetComponent<AbilitiesManager> ();
		playerAbilities.Abilities = state.Abilities;
    }

    private void LoadCameraState(CameraState state)
    {
        GameObject camera = GameObject.Find("Camera Pivot");
        camera.transform.position = state.position;
    }
}
