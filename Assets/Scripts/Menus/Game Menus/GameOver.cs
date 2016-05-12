using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : GameMenuManager
{
	public void OnGameOver(bool value)
	{
        _gameManager.SetGamePaused(!value);
        NavMenu.SetActive(value);
	}

    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
	{
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