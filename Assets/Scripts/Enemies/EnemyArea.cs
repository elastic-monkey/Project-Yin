using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class EnemyArea : MonoBehaviour
{
	public float Height = 15f;
	public Color DangerColor = Color.red;
	public Color WarningColor = Color.yellow;
	public float DangerWidth = 10f;
    public float DangerDepth = 10f;
    public float WarningWidth = 15f;
    public float WarningDepth = 15f;
	public float FightDistance;
	public int MaxEnemiesPerFight;
	public bool PlayerInWarningZone, PlayerInDangerZone;

	private GameManager _gameManager;
	protected List<EnemyBehavior> _enemiesAssigned;
	private float _halfDangerWidth, _halfWarningWidth;
	private float _halfDangerDepth, _halfWarningDepth;

	public GameManager GameManager
	{
		get
		{
			if (_gameManager == null)
				_gameManager = GameManager.Instance;

			return _gameManager;
		}
	}

	protected virtual void Awake()
	{
		_enemiesAssigned = new List<EnemyBehavior>();

		_halfDangerWidth = DangerWidth / 2;
		_halfWarningWidth = WarningWidth / 2;
		_halfDangerDepth = DangerDepth / 2;
		_halfWarningDepth = WarningDepth / 2;
	}

	protected virtual void Start()
	{
		var enemyList = GameManager.EnemiesManager.Enemies;
		foreach (var enemy in enemyList)
		{
			if (ContainsInZone(enemy.transform))
				AddEnemy(enemy);
		}
	}

	protected virtual void Update()
	{
		if (GameManager.Player == null)
			return;

		PlayerInWarningZone = ContainsInZone(GameManager.Player.transform);
		PlayerInDangerZone = ContainsInDangerZone(GameManager.Player.transform);

		if (!PlayerInWarningZone)
		{
			if (GameManager.Player.FightArea == this)
			{
				GameManager.Player.FightArea = null;
				OnSoundTransition(false);
			}
			return;
		}

		if (!PlayerInDangerZone)
			return;

		GameManager.Player.FightArea = this;

		AssignEnemiesToFight();
	}

	private void AssignEnemiesToFight()
	{
		var enemiesFighting = 0;

		_enemiesAssigned = SortEnemiesByDistanceToTarget(GameManager.Player.transform.position);

		foreach (var enemy in _enemiesAssigned)
		{
			var enemyMovement = enemy.Movement as EnemyMovement;

			if (enemiesFighting < MaxEnemiesPerFight)
			{
				enemiesFighting++;
				enemy.Attack.CanAttack = true;
				enemyMovement.Taunting = false;
			}
			else
			{
				enemy.Attack.CanAttack = false;
				enemyMovement.Taunting = true;
				enemyMovement.TauntingDistance = FightDistance;
			}
		}
	}

	private List<EnemyBehavior> SortEnemiesByDistanceToTarget(Vector3 target)
	{
		var sqrDistances = new Dictionary<EnemyBehavior, float>();
		var sortedList = new List<EnemyBehavior>();

		foreach (var enemy in _enemiesAssigned)
		{
			sqrDistances[enemy] = Vector3Helper.SqrDistanceXZ(enemy.transform.position, target);
		}

		for (var i = 0; i < _enemiesAssigned.Count; i++)
		{
			var enemy = _enemiesAssigned[i];
			var dist = sqrDistances[enemy];
			var index = -1;

			for (var j = 0; j < sortedList.Count; j++)
			{
				var other = sortedList[j];
				if (sqrDistances[other] > dist)
				{
					index = j;
					break;
				}
			}

			if (index < 0)
			{
				sortedList.Add(enemy);
			}
			else
			{
				sortedList.Insert(index, enemy);
			}
		}

		return sortedList;
	}

	public void AddEnemy(EnemyBehavior enemy)
	{
		if (!_enemiesAssigned.Contains(enemy))
		{
			_enemiesAssigned.Add(enemy);
			enemy.Area = this;
		}
	}

	public void RemoveEnemy(EnemyBehavior enemy)
	{
		_enemiesAssigned.Remove(enemy);
        if (_enemiesAssigned.Count == 0)
        {
            GameManager.SoundtrackManager.TransitionToExplore();
        }
	}

	public void NotifyEnemies(WarriorBehavior target)
	{
		OnSoundTransition(true);

		foreach (var enemy in _enemiesAssigned)
		{
			enemy.SetTarget(target);
		}
	}

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawSquareArena(transform.position, transform.rotation, DangerWidth, DangerDepth, DangerColor);
        GizmosHelper.DrawSquareArena(transform.position + 0.5f * Vector3.up, transform.rotation, DangerWidth, DangerDepth, DangerColor);
        GizmosHelper.DrawSquareArena(transform.position + Vector3.up, transform.rotation, DangerWidth, DangerDepth, DangerColor);
        GizmosHelper.DrawSquareArena(transform.position + 1.5f * Vector3.up, transform.rotation, DangerWidth, DangerDepth, DangerColor);
        GizmosHelper.DrawSquareArena(transform.position, transform.rotation, WarningWidth, WarningDepth, WarningColor);
    }

    public bool ContainsInDangerZone(Transform t)
    {
        var localPosition = transform.InverseTransformPoint(t.position);

        if (Mathf.Abs(localPosition.x) > _halfDangerWidth)
            return false;

        if (Mathf.Abs(localPosition.z) > _halfDangerDepth)
            return false;

        return true;
    }

    public bool ContainsInWarningZone(Transform t)
    {
        var localPosition = transform.InverseTransformPoint(t.position);

        if (Mathf.Abs(localPosition.x) > _halfWarningWidth)
            return false;

        if (Mathf.Abs(localPosition.z) > _halfWarningDepth)
            return false;

        return true;
    }

    public bool ContainsInZone(Transform t)
    {
        return ContainsInWarningZone(t);
    }

	protected virtual void OnSoundTransition(bool inside)
	{
		if (inside)
		{
			GameManager.SoundtrackManager.TransitionToFight();
		}
		else
		{
			GameManager.SoundtrackManager.TransitionToExplore();
		}
	}
}
