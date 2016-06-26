using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class LoadMenu : MainMenu
{
    public Text Title;
    //Title.text = value ? "New Game" : "Load Game";
    public bool IsNewGame;
    public List<GameState> SavedGames;

    protected override void Awake()
    {
        base.Awake();

        SavedGames = SaveLoad.GetAllSavedGames();
    }

    public override void Open()
    {
        base.Open();
    
        Title.text = IsNewGame ? "New Game" : "Load Game";
    }

    public void Load(int slot)
    {
        var save = GetSaveInSlot(slot);
        if (save == null)
        {
            if (IsNewGame)
            {
                LoadScene(slot, Scenes.DemoLevel.GetSceneName());
            }
        }
        else
        {
            if (IsNewGame)
            {
                SaveLoad.DeleteSaveGame(slot);
                LoadScene(slot, Scenes.DemoLevel.GetSceneName());
            }
            else
            {
                LoadScene(slot, save.CurrentScene);
            }
        }
    }

    private GameState GetSaveInSlot(int slot)
    {
        for (var i = 0; i < SavedGames.Count; i++)
        {
            if (SavedGames[i].CurrentSlot == slot)
                return SavedGames[i];
        }

        return null;
    }

    public string GetSlotName(int slot)
    {
        var save = GetSaveInSlot(slot);
        return (save == null) ? "Empty Slot" : string.Concat(save.CurrentScene, " (", save.SaveDate, ")");
    }

    private void LoadScene(int slot, string scene)
    {
        File.WriteAllText("SSS.txt", slot.ToString());
        SceneManager.LoadScene(scene);
    }
}
