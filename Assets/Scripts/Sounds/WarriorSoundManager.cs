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
    public List<AudioClipItem> Items;

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
        var item = FindItem(action);
        if (item == null)
        {
            Debug.LogWarning(string.Concat(name, ": Sound item not found for [", action.ToString(), "]"));
            return null;
        }

        if (item.Clips.Count == 0)
        {
            Debug.LogWarning(string.Concat(name, ": Found sound item not found for [", action.ToString(), "], but has no clips."));
            return null;
        }

        if (item.UseVariations)
        {
            return item.Clips[Random.Range(0, item.Clips.Count)];
        }
        else
        {
            return item.Clips[item.DefaultClip];
        }
    }

    private AudioClipItem FindItem(ClipActions action)
    {
        foreach (var item in Items)
        {
            if (item.Action.Equals(action))
                return item;
        }

        return null;
    }

    #region Editor Only

    private void OnValidate()
    {
        foreach (var item in Items)
        {
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
        public bool UseVariations;
        public int DefaultClip = 0;
        public List<AudioClip> Clips;
    }
}
