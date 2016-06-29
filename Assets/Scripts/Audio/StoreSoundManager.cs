using UnityEngine;
using System.Collections;

public class StoreSoundManager : MonoBehaviour
{
    public SoundManager SoundManager;
    [Range(0, 2)]
    public float Volume = 0.5f;
    public AudioClip OpenClip, CloseClip;

    public void PlayOpen()
    {
        SoundManager.Play(OpenClip, false, Volume);
    }

    public void PlayClose()
    {
        SoundManager.Play(CloseClip, false, Volume);
    }
}
