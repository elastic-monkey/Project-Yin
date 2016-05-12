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
            Debug.Log("SSS present. Loading savegame");
            var state = SaveLoad.Load(false);
			if (state != null)
			{
				LoadAllStates (state);
			}
			else
			{
				Debug.Log ("There is a SSS, but not a state. A New Game was started");
			}
        }
    }

    void Update()
    {
		// For testing purposes
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveLoad.Save(false);
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            SaveLoad.Save(true);
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            LoadAllStates(SaveLoad.Load(true));
        }
        else if (LoadCheckpoint)
        {
            LoadCheckpoint = false;
            LoadAllStates(SaveLoad.Load(true));
        }
    }

	public static void LoadLastCheckpoint()
	{
		GameState lastCheckpoint = SaveLoad.Load (true);
		if (lastCheckpoint != null) 
		{
			LoadAllStates (SaveLoad.Load (true));
		}
		else
		{
			Debug.Log ("No checkpoint available");
		}
	}

	public static void LoadAllStates(GameState state)
    {
        LoadPlayerState(state.PlayerState);
        LoadCameraState(state.CameraState);
    }

	public static void LoadPlayerState(PlayerState state)
    {
        var player = GameObject.Find("Player");
        var playerBehavior = player.GetComponent<PlayerBehavior>();
		var playerAnimator = player.GetComponent<Animator> ();

		playerAnimator.SetBool (AnimatorHashIDs.DeadBool, false);

        player.transform.position = state.position;
        player.transform.rotation = state.rotation;

		playerBehavior.Health.Alive = true;
        playerBehavior.Health.MaxHealth = state.MaxHealth;
        playerBehavior.Health.CurrentHealth = state.Health;
        playerBehavior.Health.CurrentLevel = state.HealthCurrentLevel;
        playerBehavior.Stamina.MaxStamina = state.MaxStamina;
        playerBehavior.Stamina.CurrentLevel = state.StaminaCurrentLevel;

        playerBehavior.Experience.CurrentExperience = state.Experience;
        playerBehavior.Experience.SkillPoints = state.SkillPoints;

        var playerAbilities = player.GetComponent<AbilitiesManager>();
        playerAbilities.RemoveAbilities();

        foreach (var sAbility in state.Abilities)
        {
            playerAbilities.Add(Ability.DeserializeAbility(sAbility));
        }
    }

	public static void LoadCameraState(CameraState state)
    {
        GameObject camera = GameObject.Find("Camera Pivot");
        camera.transform.position = state.position;
    }
}
