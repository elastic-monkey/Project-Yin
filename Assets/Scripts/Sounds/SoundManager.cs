using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public int MaxAudioSources;
    public List<AudioSource> Sources;

    private void Awake()
    {
        Sources = new List<AudioSource>();
        for (var i = 0; i < MaxAudioSources; i++)
        {
            var obj = new GameObject();
            obj.name = "Audio Source " + i;
            obj.transform.SetParent(transform, false);
            Sources.Add(obj.AddComponent<AudioSource>());
        }
    }

    public void Play(AudioClip clip, bool loop)
    {
        var ableToPlay = false;
        foreach (var source in Sources)
        {
            if (source.isPlaying)
                continue;

            ableToPlay = true;
            source.clip = clip;
            source.loop = loop;
            source.Play();
            break;
        }

        if (!ableToPlay)
            Debug.Log("Not able to play sound clip: " + clip.name + ". Consider increasing MaxAudioSources.");
    }
        
    public void Stop(AudioClip clip, bool loop = false)
    {
        foreach (var source in Sources)
        {
            if ((!loop) || (loop && source.loop))
            {
                if (source.clip == clip)
                    source.Stop();
            }
        }
    }
}


