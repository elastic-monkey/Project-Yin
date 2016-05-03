using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveGameNavItem : ButtonNavItem
{
    public int TargetSlot;

	void Start(){
		if (TargetSlot < SaveMenuManager.saves.Count) {
			GameState save = SaveMenuManager.saves [TargetSlot];
			_btnText.text = save.CurrentScene;
		} else {
			_btnText.text = "Empty Save Slot";
		}
	}

    public override void OnSelect(MainMenuManager manager)
    {
        Debug.Log("Load save game: " + TargetSlot);
		if (TargetSlot < SaveMenuManager.saves.Count) {
			GameState save = SaveMenuManager.saves [TargetSlot];
			File.WriteAllText ("SSS.txt", TargetSlot.ToString ());
			SceneManager.LoadScene (save.CurrentScene);
		} 
    }
}
