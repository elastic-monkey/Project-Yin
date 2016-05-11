using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMenu))]
public class SaveMenuManager : MenuManager
{
    public enum Actions
    {
        LoadGame,
        GoBack,
        ChangeMenu
    }

    public static List<GameState> saves;
    public SaveMenuChangeMenuNavItem OnBackPressed;

    private void Awake()
    {
        saves = SaveLoad.GetAllSavedGames();
        Debug.Log("GOT " + saves.Count + " saved games.");
    }

    private void Start()
    {
        NavMenu.SetActive(NavMenu.IsActive);
    }

    private void Update()
    {
        if (!NavMenu.IsActive)
            return;
        
        if (PlayerInput.IsButtonUp(Axis.Escape))
        {
            OnAction(OnBackPressed, OnBackPressed.Action, OnBackPressed.Target);
        }
    }
        
    public override void OnFocus(NavItem target)
    {
        // TODO
    }

    public override void OnAction(NavItem item, object action, object data)
    {
        var actionEnum = (Actions)action;

        switch (actionEnum)
        {
            case Actions.LoadGame:
                var slot = (int)data;
                if (slot < SaveMenuManager.saves.Count)
                {
                    var save = SaveMenuManager.saves[slot];
                    File.WriteAllText("SSS.txt", slot.ToString());
                    SceneManager.LoadScene(save.CurrentScene);
                } 
                break;

            case Actions.ChangeMenu:
                var target = (NavMenu)data;
                target.SetActive(true);
                NavMenu.SetActive(false);
                break;
        }
    }
}
