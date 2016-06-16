using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
	public enum Moments
	{
		Explore,
		Fight
	}

	public SoundManager SoundManager;
	public AudioClip[] Explore;
	public AudioClip ExploreToExplore;
	public AudioClip ExploreToFight;
	public AudioClip[] Fight;
	public AudioClip FightToFight;
	public AudioClip FightToExplore;
	public float FadeOutDuration = 1f;
	public float FadeInDuration = 1f;
	[Range(0, 1)]
	public float Volume = 0.4f;
	public AudioState CurrentState;

	private AudioClip CurrentClip
	{
		get
		{
			switch (CurrentState.Moment)
			{
				case Moments.Fight:
					return Fight[CurrentState.ClipIndex];

				case Moments.Explore:
					return Explore[CurrentState.ClipIndex];
			}

			return null;
		}
	}

	private void Awake()
	{
		CurrentState = new AudioState(Moments.Explore, 0);
	}

	private void Start()
	{
		SoundManager.FadeIn(CurrentClip, FadeInDuration, Volume);
		CurrentState.DeltaTime = 0f;
	}

	public void Update()
	{
		CurrentState.DeltaTime += Time.deltaTime;
		if (CurrentState.DeltaTime + 1 >= CurrentClip.length)
		{
			TransitionToNextVariation();
		}
	}

	public void TransitionToNextVariation()
	{
		if (CurrentState.Moment.Equals(Moments.Fight))
		{
			if (Fight.Length == 1)
				return;

			SoundManager.FadeOut(CurrentClip, FadeOutDuration);

			if (CurrentState.ClipIndex + 1 >= Fight.Length)
				CurrentState.ClipIndex = 0;
			else
				CurrentState.ClipIndex += 1;

			CurrentState.ResetDeltaTime();
			SoundManager.Play(FightToFight, false, Volume);
			SoundManager.FadeIn(CurrentClip, FadeInDuration, Volume);
		}
		else if (CurrentState.Moment.Equals(Moments.Explore))
		{
			if (Explore.Length == 1)
				return;

			SoundManager.FadeOut(CurrentClip, FadeOutDuration);

			if (CurrentState.ClipIndex + 1 >= Explore.Length)
				CurrentState.ClipIndex = 0;
			else
				CurrentState.ClipIndex += 1;

			CurrentState.ResetDeltaTime();
			SoundManager.Play(ExploreToExplore, false, Volume);
			SoundManager.FadeIn(CurrentClip, FadeInDuration, Volume);
		}
	}

	public void TransitionToFight()
	{
		if (CurrentState.Moment.Equals(Moments.Fight))
			return;

		if (Fight.Length == 0)
			return;

		SoundManager.FadeOut(CurrentClip);

		CurrentState.Moment = Moments.Fight;
		CurrentState.ClipIndex = Random.Range(0, Fight.Length);
		CurrentState.ResetDeltaTime();

		SoundManager.Play(ExploreToFight, false, Volume * 0.5f);
		SoundManager.FadeIn(CurrentClip, FadeInDuration, Volume);
	}

	public void TransitionToExplore()
	{
		if (CurrentState.Moment.Equals(Moments.Explore))
			return;

		if (Explore.Length == 0)
			return;

		SoundManager.FadeOut(CurrentClip);

		CurrentState.Moment = Moments.Explore;
		CurrentState.ClipIndex = Random.Range(0, Explore.Length);

		SoundManager.Play(FightToExplore, false, Volume);
		SoundManager.FadeIn(CurrentClip, FadeInDuration, Volume);
	}

	[System.Serializable]
	public struct AudioState
	{
		public Moments Moment;
		public int ClipIndex;
		public float DeltaTime;

		public void ResetDeltaTime()
		{
			DeltaTime = 0f;
		}

		public AudioState(Moments moment, int index)
		{
			Moment = moment;
			ClipIndex = index;
			DeltaTime = 0f;
		}
	}
}
