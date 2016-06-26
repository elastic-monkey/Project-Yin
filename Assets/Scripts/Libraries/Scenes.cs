using UnityEngine;
using System.Collections.Generic;

public enum Scenes
{
    MainMenu,
    DemoLevel
}

public static class ScenesHelper
{
    private static readonly Dictionary<Scenes, string> _scenesNames = new Dictionary<Scenes, string>() {
        { Scenes.MainMenu, "MainMenu" },
        { Scenes.DemoLevel, "Final_Level" }
    };

    public static string GetSceneName(this Scenes scene)
    {
        var name = string.Empty;

        _scenesNames.TryGetValue(scene, out name);

        return name;
    }
}
