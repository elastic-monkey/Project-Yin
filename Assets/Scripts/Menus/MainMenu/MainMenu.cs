using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public ShowHidePanel Main, LoadMenu, QuitGame;
    ShowHidePanel _currentPanel;

    void Start()
    {
        ChangeCurrentPanelTo(Main);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            OnBackPressed();
    }

    public void OnBackPressed()
    {
        if (_currentPanel == Main)
            OnQuitPressed();
        else
            ChangeCurrentPanelTo(Main);
    }

    public void OnQuitPressed()
    {
        ChangeCurrentPanelTo(QuitGame);
    }

    public void OnQuitCancelled()
    {
        ChangeCurrentPanelTo(Main);
    }

    public void OnQuitConfirmed()
    {
        Application.Quit();
    }
		
    public void OnLoadMenuPressed()
    {
		ChangeCurrentPanelTo(LoadMenu);
    }

    private void ChangeCurrentPanelTo(ShowHidePanel newPanel)
    {
        Main.Visible = (newPanel == Main);
		LoadMenu.Visible = (newPanel == LoadMenu);
		QuitGame.Visible = (newPanel == QuitGame);

        _currentPanel = newPanel;
    }
}
