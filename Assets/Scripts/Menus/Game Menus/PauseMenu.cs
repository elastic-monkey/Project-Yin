
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : GameMenuManager
{
    protected override void OnAction(Actions action, NavItem item, NavMenu target, string[] data)
    {
        var actionEnum = (Actions)action;

        switch (actionEnum)
        {
            case Actions.Pause:
                OnPause(true);
                break;

            case Actions.Resume:
                OnPause(false);
                break;

            case Actions.LoadLastCheckpoint:
                SaveManager.LoadLastCheckpoint();
                OnPause(false);
                break;

            case Actions.Back:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}