using UnityEngine;

public class MainMenu : Menu
{
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
    
        if (CloseSubmenus != Axes.None && PlayerInput.IsButtonDown(CloseSubmenus))
        {
            CloseIfSubmenu();
        }
    }

    public override void OnNavItemFocused(NavItem target)
    {
        SoundManager.PlayFocusItemSound();
    }
}
