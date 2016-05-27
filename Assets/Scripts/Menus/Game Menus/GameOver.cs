using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : GameMenuManager
{
	public void OnGameOver(bool value)
	{
        GameManager.SetGamePaused(!value);
        NavMenu.SetActive(value);
	}

    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
	{
        var action = (Actions)actionObj;

        switch (action)
		{
			case Actions.LoadLastCheckpoint:
				Debug.Log ("Loading last checkpoint");
				SaveManager.LoadLastCheckpoint ();
				OnGameOver(false);
				break;

            case Actions.Back:
				Debug.Log ("Loading last checkpoint");
				SceneManager.LoadScene("MainMenu");
				break;
		}
    }
}