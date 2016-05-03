
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
	public Button Resume, LoadLastCheckpoint, Settings, ReturnToMainMenu;
	private ShowHidePanel _panel;

	void Start()
	{
		_panel = GetComponent<ShowHidePanel>();
		_panel.SetVisible(false);
		SetInteractable (false);
	}

	void Update()
	{
		if (PlayerInput.IsButtonDown(Axis.Escape))
		{
            PlayerInput.GameplayBlocked = true;
			Time.timeScale = 0f;
			_panel.SetVisible(true);
			SetInteractable (true);
		}
	}

	public void OnResumePressed()
	{
		SetInteractable (false);
		_panel.SetVisible(false);
        PlayerInput.GameplayBlocked = false;
		Time.timeScale = 1f;
	}

	public void OnLoadLastCheckpointPressed()
	{
		SaveManager.LoadCheckpoint = true;
		OnResumePressed ();
	}

	public void OnReturnToMainMenuPressed(){
		SceneManager.LoadScene ("MainMenu");
	}

	private void SetInteractable(bool interact){
		Resume.interactable = interact;
		LoadLastCheckpoint.interactable = interact;
		Settings.interactable = interact;
		ReturnToMainMenu.interactable = interact;
	}
}
