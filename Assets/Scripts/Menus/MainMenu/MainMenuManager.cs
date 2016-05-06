using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MenuManager
{
    public enum Actions
    {
        NewGame,
        SelectMenu,
        LoadGame
    }

    public MainMenuItems[] Items;

    [SerializeField]
    private int _currentMenu;

    private void Start()
    {
        SelectMenu(0);
    }

    private void Update()
    {
        if (PlayerInput.IsButtonUp(Axis.Escape))
        {
            OnBackPressed();
        }
    }

    private void SelectMenu(int index)
    {
        index = Mathf.Clamp(index, 0, Items.Length - 1);

        for (var i = 0; i < Items.Length; i++)
        {
            Items[i].Menu.SetActive(i == index);
        }

        _currentMenu = index;
    }

    public void SelectMenu(VerticalNavMenu menu)
    {
        for (var i = 0; i < Items.Length; i++)
        {
            if (Items[i].Menu == menu)
            {
                SelectMenu(i);
                break;
            }
        }
    }

    public void OnBackPressed()
    {
        var previous = Items[_currentMenu].Previous;

        if (previous.IsNull())
        {
            Debug.LogWarning("Back: there is no previous to go to.");
        }
        else
        {
            SelectMenu(previous);
        }
    }

    [System.Serializable]
    public class MainMenuItems
    {
        public VerticalNavMenu Previous;
        public VerticalNavMenu Menu;
    }

    public override void OnAction(object action, object data)
    {
        var actionEnum = (Actions)action;
    
        switch (actionEnum)
        {
            case Actions.NewGame:
                SceneManager.LoadScene("Level_1");
                break;

            case Actions.LoadGame:
                break;

            case Actions.SelectMenu:
                var target = (VerticalNavMenu)data;
                SelectMenu(target);
                break;
        }
    }
}
