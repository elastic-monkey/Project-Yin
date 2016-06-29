using UnityEngine;

[RequireComponent(typeof(SoundManager))]
public class MenuSoundManager : MonoBehaviour
{
	[Range(0, 1)]
	public float OpenVolume = 0.5f;
	public AudioClip[] OpenSounds;
	[Range(0, 1)]
	public float CloseVolume = 0.5f;
	public AudioClip[] CloseSounds;
	[Range(0, 1)]
	public float FocusVolume = 0.5f;
	public AudioClip[] FocusSounds;
	public bool InGame = true;

	private SoundManager _soundManager;

	private void Awake()
	{
		_soundManager = GetComponent<SoundManager>();
	}

	public void PlayOpenSound()
	{
		_soundManager.Play(OpenSounds[Random.Range(0, OpenSounds.Length)], false, OpenVolume);
	}

	public void PlayFocusItemSound()
	{
		if (InGame && !PlayerInput.OnlyMenus)
			return;

		_soundManager.Play(FocusSounds[Random.Range(0, FocusSounds.Length)], false, FocusVolume);
	}

	public void PlayCloseSound()
	{
		_soundManager.Play(CloseSounds[Random.Range(0, CloseSounds.Length)], false, CloseVolume);
	}

	public void PlaySampleSound(float volume)
	{
		_soundManager.Play(CloseSounds[0], false, volume);
	}
}
