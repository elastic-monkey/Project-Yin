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
        var player = GameObject.Find("Yin");
        var playerBehavior = player.GetComponent<PlayerBehavior>();
		var playerAnimator = player.GetComponent<Animator> ();
        var inventory = GameObject.Find("InventorySubMenu").GetComponent<InventoryMenu>();
        var itemRepo = GameObject.Find("ItemRepo").GetComponent<ItemRepo>();

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

        playerBehavior.Currency.CurrentCredits = state.Credits;

        var playerAbilities = player.GetComponent<AbilitiesManager>();
        playerAbilities.RemoveAbilities();

        foreach (var sAbility in state.Abilities)
        {
            playerAbilities.Add(Ability.DeserializeAbility(sAbility));
        }

        for (var i = 0; i < inventory.InventorySlots.Count; i++)
        {
            var loadSlot = state.Inventory[i];
            var slot = inventory.InventorySlots[i];
            if (loadSlot.Type != Item.ItemType.Null && loadSlot.Stock != 0)
            {
                var item = itemRepo.Find(loadSlot.Type);
                slot.Item = item;
                slot.Stock = loadSlot.Stock;
                slot.UpdateSlot();
            }
        }

    }

	public static void LoadCameraState(CameraState state)
    {
        GameObject camera = GameObject.Find("Camera Pivot");
        camera.transform.position = state.position;
    }
}
