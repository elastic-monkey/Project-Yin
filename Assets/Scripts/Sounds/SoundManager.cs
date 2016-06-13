using UnityEngine;
using System.Collections;
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
			var source = obj.AddComponent<AudioSource>();
			source.playOnAwake = false;
			Sources.Add(source);
		}
	}

	private AudioSource FindAvailableSource()
	{
		foreach (var source in Sources)
		{
			if (source.isPlaying)
				continue;

			return source;
		}

		return null;
	}

	private AudioSource FindSourceByClip(AudioClip clip)
	{
		foreach (var source in Sources)
		{
			if (clip.Equals(source.clip))
				return source;
		}

		return null;
	}

	public AudioSource Play(AudioClip clip, bool loop, float volume = 1f)
	{
		var source = FindAvailableSource();
		if (source == null)
		{
			Debug.LogWarning("Not able to play sound clip: " + clip.name + ". Consider increasing MaxAudioSources.");
			return null;
		}

		source.volume = volume;
		source.clip = clip;
		source.loop = loop;
		source.Play();

		if (!loop)
			StartCoroutine(CleanAfterPlaying(source, clip.length));

		return source;
	}

	public void Stop(AudioClip clip)
	{
		var source = FindSourceByClip(clip);
		if (source == null)
			return;

		Stop(source);
	}

	public void Stop(AudioSource source)
	{
		source.Stop();
		source.clip = null;
	}

	public void FadeOut(AudioClip clip, float duration = 1f)
	{
		var source = FindSourceByClip(clip);
		if (source == null)
		{
			Debug.LogWarning("Fade out: no suitable source found for clip: " + clip.name);
			return;
		}

		StartCoroutine(FadeVolume(source, 0f, duration));
	}

	public void FadeIn(AudioClip clip, float duration = 1f, float maxVolume = 1f, bool loop = false)
	{
		var source = FindSourceByClip(clip);
		if (source != null)
		{
			Debug.LogWarning("Fade in: clip already has a source.");
			return;
		}

		source = Play(clip, loop);
		if (source == null)
			return;

		source.volume = 0f;

		StartCoroutine(FadeVolume(source, Mathf.Min(1f, maxVolume), duration));
	}

	private IEnumerator FadeVolume(AudioSource source, float targetVolume, float duration)
	{
		var invDuration = 1f / duration;
		var deltaTime = 0f;
		var beginVolume = source.volume;

		while (deltaTime < duration)
		{
			source.volume = Mathf.Lerp(beginVolume, targetVolume, deltaTime * invDuration);

			yield return null;

			deltaTime += Time.deltaTime;
		}

		source.volume = targetVolume;
		if (targetVolume <= 0f)
		{
			Stop(source);
		}
	}

	private IEnumerator CleanAfterPlaying(AudioSource source, float duration)
	{
		float deltaTime = 0f;
		while (deltaTime <= duration)
		{
			deltaTime += Time.deltaTime;
			yield return null;
		}

		Stop(source);
	}
}


