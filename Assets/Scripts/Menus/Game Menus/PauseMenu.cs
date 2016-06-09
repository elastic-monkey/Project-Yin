using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : GameMenu
{
    public override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.Resume:
                Close();
                return true;

            case Actions.LoadLastCheckpoint:
                SaveManager.LoadLastCheckpoint();
                Close();
                return true;

            case Actions.GoToMainMenu:
                SceneManager.LoadScene("MainMenu");
                return true;
        }

        return false;
    }
}