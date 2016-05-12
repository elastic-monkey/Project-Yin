using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class LoadMenu : MainMenuManager
{
    public static List<GameState> SavedGames;

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
                if(int.TryParse(data[0], out slot) && slot < SavedGames.Count)
                {
                    // Customized usage of "data" array goes here
                    var save = SavedGames[slot];
                    File.WriteAllText("SSS.txt", slot.ToString());
                    SceneManager.LoadScene(save.CurrentScene);
                } 
                break;

            case Actions.Back:
                TransitionTo(target);
                break;
        }
    }
}
