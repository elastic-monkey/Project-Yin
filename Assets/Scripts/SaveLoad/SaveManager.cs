using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

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
				SetDefaultStaticValues();
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

    public static void LoadLastSave()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

	public static void SetDefaultStaticValues()
	{
		Time.timeScale = 1.0f;
		PlayerInput.OnlyMenus = false;
	}

	public static void LoadAllStates(GameState state)
    {
		SetDefaultStaticValues();
		LoadPlayerState(state.PlayerState);
        LoadCameraState(state.CameraState);
    }

	public static void LoadPlayerState(PlayerState state)
    {
        var player = GameManager.Instance.Player;
		var playerAnimator = player.GetComponent<Animator> ();
        var inventory = player.Inventory;
        var itemRepo = GameManager.Instance.ItemRepo;

		playerAnimator.SetBool (AnimatorHashIDs.DeadBool, false);

        player.transform.position = state.position;
        player.transform.rotation = state.rotation;

        player.Health.Alive = true;
        player.Health.MaxHealth = state.MaxHealth;
        player.Health.CurrentHealth = state.Health;
        player.Health.CurrentLevel = state.HealthCurrentLevel;
        player.Stamina.MaxStamina = state.MaxStamina;
        player.Stamina.CurrentStamina = state.MaxStamina;
        player.Stamina.CurrentLevel = state.StaminaCurrentLevel;

        player.Experience.CurrentExperience = state.Experience;
        player.Experience.SkillPoints = state.SkillPoints;

        player.Currency.CurrentCredits = state.Credits;

        var playerAbilities = player.GetComponent<AbilitiesManager>();
        foreach (var sAbility in state.Abilities)
        {
            playerAbilities.Set(sAbility);
        }

        inventory.Slots.Clear();
        foreach (var sSlot in state.InventorySlots)
        {
            var item = itemRepo.Find(sSlot.Type);
            var exists = false;

            foreach (var slot in inventory.Slots)
            {
                if (slot.Type == item.Type)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
                continue;

            var itemSlot = new PlayerInventory.ItemSlot();
            itemSlot.Type = sSlot.Type;
            itemSlot.Stock = sSlot.Stock;

            inventory.Slots.Add(itemSlot);
        }
    }

	public static void LoadCameraState(CameraState state)
    {
        var camera = GameObject.Find("Camera Pivot");
        camera.transform.position = state.position;
    }
}
