using UnityEngine;
using System.Collections;

public class AmbientSoundManager : MonoBehaviour
{
    public SoundManager SoundManager;

    [Range(0, 2)]
    public float AirVolume = 1;
    public AudioClip AirClip;
    public AudioClipEffect[] Effects;

    public GameManager GameManager
    {
        get
        {
            return GameManager.Instance;
        }
    }

    private void Start()
    {
        SoundManager.Play(AirClip, true, AirVolume);

        foreach (var effect in Effects)
        {
            StartCoroutine(EffectCoroutine(effect));
        }
    }

    private IEnumerator EffectCoroutine(AudioClipEffect effect)
    {
        while (true)
        {
            var value = Random.Range(0f, 1f);

            if (value < effect.Probability)
            {
                var pos = GameManager.Player.transform.position + Random.Range(2f, 8f) * new Vector3(1, 0, 1);
                SoundManager.PlaySpatial(pos, effect.Clips[Random.Range(0, effect.Clips.Length)], false, 0.95f, 50, effect.Volume);
            }

            yield return new WaitForSeconds(effect.Interval);
        }
    }

    [System.Serializable]
    public class AudioClipEffect
    {
        public string Name;
        [Range(0, 2)]
        public float Volume = 0.5f;
        [Range(0, 1)]
        public float Probability = 0.5f;
        public float Interval = 5;
        public AudioClip[] Clips;
    }
}
