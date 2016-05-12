using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class LoadMenu : MainMenuManager
{
    public static List<GameState> SavedGames;
	private bool _newGameMode;
	public Text Title;

	public bool NewGameMode
	{
		get
		{
			return _newGameMode;
		}
		set
		{
			_newGameMode = value;
			Title.text = value ? "New Game" : "Load Game";
		}
	}

    private void Awake()
    {
        SavedGames = SaveLoad.GetAllSavedGames();
        Debug.Log("Got " + SavedGames.Count + " saved games.");
    }

    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        switch (action)
        {
            case Actions.Load:
                var slot = -1;
                if(int.TryParse(data[0], out slot))
				{
					var save = GetSaveInSlot (slot);
					if (save != null)
					{
						if (_newGameMode)
						{
							Debug.Log ("A Save exists in slot. Will be deleted");
							SaveLoad.DeleteSaveGame (slot);
							LoadScene (slot, "TestArena");
						}
						else
						{
							LoadScene (slot, save.CurrentScene);
						}
					}
					else
					{
						if (_newGameMode)
						{
							Debug.Log ("Starting a New Game");
							LoadScene (slot, "TestArena");
						}
						else
						{
							Debug.Log("Empty Slot. Nothing to Load");
						}
					}
				}
                break;

            case Actions.Back:
                TransitionTo(target);
                break;
        }
    }

	public static GameState GetSaveInSlot(int slot)
	{
		for (var i = 0; i < SavedGames.Count; i++)
		{
			if (SavedGames [i].CurrentSlot == slot)
			{
				return SavedGames [i];
			}
		}
		return null;
	}

	private void LoadScene(int slot, string scene){
		File.WriteAllText("SSS.txt", slot.ToString());
		SceneManager.LoadScene(scene);
	}
}
