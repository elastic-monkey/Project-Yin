using UnityEngine;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour
{
	[SerializeField]
	private List<EnemyBehavior> _enemies;

	public int EnemyCount
	{
		get
		{
			if (_enemies == null)
				return 0;

			return _enemies.Count;
		}
	}

	private void Awake()
	{
		_enemies = new List<EnemyBehavior>(FindObjectsOfType<EnemyBehavior>());

		var player = FindObjectOfType<PlayerBehavior>();

		foreach (var enemy in _enemies)
		{
			enemy.Target = player;
		}
	}

	public void RemoveEnemy(EnemyBehavior enemy)
	{
		_enemies.Remove(enemy);
	}

	public void AddEnemy(EnemyBehavior enemy)
	{
		if (!_enemies.Contains(enemy))
			_enemies.Add(enemy);
	}

	public void AddEnemies(EnemyBehavior[] enemies)
	{
		foreach (var enemy in enemies)
		{
			if (!_enemies.Contains(enemy))
				_enemies.Add(enemy);
		}
	}
}
