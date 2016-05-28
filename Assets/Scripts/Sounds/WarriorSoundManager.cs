using UnityEngine;
using System.Collections.Generic;

public class WarriorSoundManager : MonoBehaviour
{
    public enum ClipActions
    {
        Step,
        WeaponStrike,
        NULL
    }

    public SoundManager SoundManager;
    public bool PlayVariations;
    public List<AudioClipItem> Clips;

    public void PlayClip(ClipActions action)
    {
        Play(action, false);
    }

    public void Play(ClipActions action, bool loop)
    {
        var clip = FindClip(action);
        if (clip == null)
            return;

        SoundManager.Play(clip, loop);
    }

    public void Stop(ClipActions action)
    {
        var clip = FindClip(action);
        if (clip == null)
            return;

        SoundManager.Stop(clip);
    }

    private AudioClip FindClip(ClipActions action)
    {
        if (PlayVariations)
        {
            var index = -1;
            for (var i = 0; i < Clips.Count; i++)
            {
                var clip = Clips[i];
                if (clip.Action.Equals(action))
                {
                    index = (index < 0) ? i : (Random.Range(0, 2) < 1) ? index : i;
                }
            }

            if (index >= 0)
            {
                return Clips[index].Clip;
            }
        }
        else
        {
            foreach (var clip in Clips)
            {
                if (clip.Action.Equals(action))
                    return clip.Clip;
            }
        }

        return null;
    }

    #region Editor Only

    private void OnValidate()
    {
        foreach (var item in Clips)
        {
            if (item.Clip != null)
                item.Name = string.Concat(item.Action.ToString(), " --> ", item.Clip.name);
            else
                item.Name = item.Action.ToString();
        }
    }

    #endregion

    [System.Serializable]
    public class AudioClipItem
    {
        [HideInInspector]
        public string Name;
        public ClipActions Action;
        public AudioClip Clip;
    }
}
