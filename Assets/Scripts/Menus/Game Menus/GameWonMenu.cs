using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonMenu : GameMenu
{
	public override void Open()
	{
		base.Open();

		SaveLoad.Save(false);
	}

	public override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.Resume:
                Debug.Log("Resuming game...");
                return true;

            case Actions.Close:
                Debug.Log("Loading main menu...");
                SceneManager.LoadScene("MainMenu");
                return true;
        }

        return false;
    }
}