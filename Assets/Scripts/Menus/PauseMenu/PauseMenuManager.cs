
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MenuManager
{
    public enum Actions
    {
        Resume,
        LoadLastCheckpoint,
        Settings,
        ReturnToMainMenu
    }

    public bool GamePaused;
    public VerticalNavMenu PauseMenu;

    private void Awake()
    {
        GamePaused = false;
    }

    private void Update()
    {
        if (PlayerInput.IsButtonDown(Axis.Escape))
        {
            SetGamePaused(!GamePaused);
        }
    }

    private void SetGamePaused(bool value)
    {
        GamePaused = value;
        PlayerInput.GameplayBlocked = value;
        Time.timeScale = value ? 0 : 1;
        PauseMenu.SetActive(value);
    }

    public override void OnAction(object action, object data)
    {
        var actionEnum = (Actions)action;

        switch (actionEnum)
        {
            case Actions.Resume:
                SetGamePaused(false);
                break;

            case Actions.LoadLastCheckpoint:
                SaveManager.LoadCheckpoint = true;
                SetGamePaused(false);
                break;

            case Actions.Settings:
                Debug.Log("Not implemented settings yet");
                break;

            case Actions.ReturnToMainMenu:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
