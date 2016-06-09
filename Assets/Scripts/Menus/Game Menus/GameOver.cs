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

    protected override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.LoadLastCheckpoint:
                Debug.Log("Loading last checkpoint...");
                SaveManager.LoadLastCheckpoint();
                OnGameOver(false);
                return true;

            case Actions.Close:
                Debug.Log("Loading main menu...");
                SceneManager.LoadScene("MainMenu");
                return true;
        }

        return false;
    }
}