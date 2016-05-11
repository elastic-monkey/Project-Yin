
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverMenuManager : MenuManager
{
	public enum Actions
	{
		LoadLastCheckpoint,
		ReturnToMainMenu
	}

	public VerticalNavMenu GameOverMenu;

	private GameManager _gameManager;

	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	/*private void Update()
	{
		if (PlayerInput.IsButtonDown(Axis.Escape))
		{
			if (_gameManager.GamePaused)
			{
				//TODO This
			}
			else
			{
				GameOver(true);
			}
		}
	}*/

	public void GameOver(bool value)
	{
		GameOverMenu.SetActive(value);
	}

	public override void OnFocus(NavItem target)
	{
		// Do stuff
	}

	public override void OnAction(NavItem item, object action, object data)
	{
		var actionEnum = (Actions)action;

		switch (actionEnum)
		{
			case Actions.LoadLastCheckpoint:
				Debug.Log ("Loading last checkpoint");
				SaveManager.LoadLastCheckpoint ();
				GameOver(false);
				break;

			case Actions.ReturnToMainMenu:
				Debug.Log ("Loading last checkpoint");
				SceneManager.LoadScene("MainMenu");
				break;
		}
	}
}
