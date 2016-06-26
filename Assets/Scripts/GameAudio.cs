using UnityEngine;
using System.Collections;

public static class GameAudio
{
    public enum AudioFacets
    {
        Menus,
        Soundtrack,
        SFX
    }

    public static float MenusVolume = 1f;
    public static float SoundtrackVolume = 1f;
    public static float SFXVolume = 1f;

    public static void ResetAll()
    {
        MenusVolume = 1f;
        SoundtrackVolume = 1f;
        SFXVolume = 1f;
    }

    public static void MuteAll()
    {
        MenusVolume = 0f;
        SoundtrackVolume = 0f;
        SFXVolume = 0f;
    }

    public static void Change(AudioFacets type, float value)
    {
        switch (type)
        {
            case AudioFacets.Menus:
                MenusVolume = Mathf.Clamp01(value);
                break;

            case AudioFacets.Soundtrack:
                SoundtrackVolume = Mathf.Clamp01(value);
                break;

            case AudioFacets.SFX:
                SFXVolume = Mathf.Clamp01(value);
                break;
        }
    }
}
