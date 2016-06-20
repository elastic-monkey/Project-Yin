using UnityEngine;
using System.Collections;

public class BossArea : EnemyArea
{
	public SlideBuilding[] Barriers;
	public bool TrapEnabled;
	public bool EnemiesAreAlive;

	protected override void Start()
	{
		base.Start();

		EnemiesAreAlive = true;
		SetTrapEnabled(false);
	}

	protected override void Update()
	{
		base.Update();

		EnemiesAreAlive = false;
		foreach (var enemy in _enemiesAssigned)
		{
			if (enemy.Health.Alive)
			{
				EnemiesAreAlive = true;
				break;
			}
		}

		if (TrapEnabled)
		{
			if (!EnemiesAreAlive) // all enemies are dead
				SetTrapEnabled(false);
		}
		else
		{
			if (!PlayerInDangerZone)
				return;

			if (!EnemiesAreAlive)
				return;

			SetTrapEnabled(true);
		}
	}

	private void SetTrapEnabled(bool value)
	{
		if (TrapEnabled == value)
			return;

		TrapEnabled = value;
		OnSoundTransition(value);

		foreach (var b in Barriers)
		{
			if (value)
				b.Show();
			else
				b.Hide();
		}
	}

	protected override void OnSoundTransition(bool inside)
	{
		// disregard inside value

		if (TrapEnabled)
		{
			GameManager.SoundtrackManager.TransitionToFight();
		}
		else
		{
			GameManager.SoundtrackManager.TransitionToExplore();
		}
	}
}
