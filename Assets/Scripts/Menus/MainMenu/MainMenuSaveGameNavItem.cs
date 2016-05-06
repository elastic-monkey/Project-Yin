using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuSaveGameNavItem : TextNavItem
{
    public int TargetSlot;

    void Start()
    {
        if (TargetSlot < SaveMenuManager.saves.Count)
        {
            GameState save = SaveMenuManager.saves[TargetSlot];
            _text.text = save.CurrentScene;
        }
        else
        {
            _text.text = "Empty Save Slot";
        }
    }

    public override void OnSelect(MenuManager manager)
    {
        Debug.Log("Load save game: " + TargetSlot);

        if (TargetSlot < SaveMenuManager.saves.Count)
        {
            GameState save = SaveMenuManager.saves[TargetSlot];
            File.WriteAllText("SSS.txt", TargetSlot.ToString());
            SceneManager.LoadScene(save.CurrentScene);
        } 
    }
}
