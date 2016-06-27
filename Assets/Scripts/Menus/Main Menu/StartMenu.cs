using UnityEngine;

public class StartMenu : MainMenu
{
    public NavMenu SettingsSubmenu;

    public override void Close()
    {
        if (CurrentNavMenu == SettingsSubmenu)
        {
            SaveLoad.SaveAudioSettings();    
        }

        base.Close();
    }
}
