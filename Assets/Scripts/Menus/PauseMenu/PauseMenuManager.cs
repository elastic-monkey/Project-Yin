
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

    public VerticalNavMenu PauseMenu;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (PlayerInput.IsButtonDown(Axis.Escape))
        {
            if (_gameManager.GamePaused)
            {
                // This verification is necessary for this menu not to open if,
                //  for example, upgrade menu is already open.
                if (PauseMenu.IsActive)
                {
                    OnPause(false);
                }
            }
            else
            {
                OnPause(true);
            }
        }
    }

    private void OnPause(bool value)
    {
        _gameManager.SetGamePaused(value);
        PauseMenu.SetActive(value);
    }

    public override void OnAction(object action, object data)
    {
        var actionEnum = (Actions)action;

        switch (actionEnum)
        {
            case Actions.Resume:
                OnPause(false);
                break;

            case Actions.LoadLastCheckpoint:
                SaveManager.LoadCheckpoint = true;
                OnPause(false);
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
