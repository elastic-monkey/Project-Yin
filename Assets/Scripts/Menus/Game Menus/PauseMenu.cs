using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : GameMenuManager
{
    protected override void OnNavItemAction(object actionObj, NavItem item, NavMenu target, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
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