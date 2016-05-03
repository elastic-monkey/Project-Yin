using UnityEngine;
using System.Collections.Generic;

public class LoadMenu : NavMenu
{
    public List<GameState> SavedGames;
    public int CurrentOption;

    protected override void Awake()
    {
        base.Awake();

        SavedGames = SaveLoad.GetAllSavedGames();
        CurrentOption = 0;
    }

    public override void HandleInput()
    {
        if (!_active)
            return;

        var v = PlayerInput.GetAxis(Axis.Vertical);

        if (v > 0)
        {
            SelectNext();
        }
        else if(v < 0)
        {
            SelectPrevious();
        }
    }

    private void SelectOption(int option)
    {
        CurrentOption = option;
        // TODO: Select appropriate slot visually
    }

    private void SelectNext()
    {
        if (CurrentOption < SavedGames.Count)
        {
            SelectOption(CurrentOption + 1);
        }
        else
        {
            if (Cyclic)
            {
                SelectOption(0);
            }
            else
            {
                SelectOption(SavedGames.Count - 1);
            }
        }
    }

    private void SelectPrevious()
    {
        if (CurrentOption > 0)
        {
            SelectOption(CurrentOption - 1);
        }
        else
        {
            if (Cyclic)
            {
                SelectOption(SavedGames.Count - 1);
            }
            else
            {
                SelectOption(0);
            }
        }
    }
        
    protected override void OnSetActive(bool value)
    {
        SelectOption(0);
    }
}
