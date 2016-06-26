using UnityEngine;

public class MainMenu : Menu
{
    public MainMenu PreviousMenu;
    public Axes CloseSubmenus = Axes.Back;
    public bool OpenOnStart;

    protected MenuSoundManager SoundManager
    {
        get
        {
            return MainMenuManager.Instance.SoundManager;
        }
    }

    protected override void Start()
    {
        base.Start();
    
        if (OpenOnStart)
        {
            Open();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!IsOpen)
            return;
    
        if (CloseSubmenus != Axes.None && PlayerInput.IsButtonDown(CloseSubmenus))
        {
            var submenu = CloseIfSubmenu();
            if (!submenu && PreviousMenu != null)
            {
                Close();
                PreviousMenu.Open();
            }
        }
    }

    public override void OnNavItemFocused(NavItem target, bool silent = false)
    {
        if (!IsOpen)
            return;
        
        if (!silent)
            SoundManager.PlayFocusItemSound();
    }
}
