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

    public static void Change(AudioFacets facet, float value)
    {
        switch (facet)
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

    public static float GetVolume(AudioFacets facet)
    {
        switch (facet)
        {
            case AudioFacets.Menus:
                return MenusVolume;

            case AudioFacets.Soundtrack:
                return SoundtrackVolume;

            case AudioFacets.SFX:
                return SFXVolume;
        }

        return 0;
    }

    public static SerializableAudioSettings Serialize()
    {
        var obj = new SerializableAudioSettings();
        obj.SFX = SFXVolume;
        obj.Menus = MenusVolume;
        obj.Soundtrack = SoundtrackVolume;

        return obj;
    }

    public static void LoadFromSerialized(SerializableAudioSettings settings)
    {
        MenusVolume = settings.Menus;
        SFXVolume = settings.SFX;
        SoundtrackVolume = settings.Soundtrack;
    }
}

[System.Serializable]
public class SerializableAudioSettings
{
    public float SFX, Soundtrack, Menus;
}