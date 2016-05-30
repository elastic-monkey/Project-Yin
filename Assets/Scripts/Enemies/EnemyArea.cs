using UnityEngine;
using System.Collections.Generic;
using Utilities;

public abstract class EnemyArea : MonoBehaviour
{
    public Color DangerColor = Color.red;
    public Color WarningColor = Color.yellow;
    public bool PlayerInWarningZone;
    public bool PlayerInDangerZone;
    public float FightDistance;
    public int MaxEnemiesPerFight;

    [SerializeField]
    protected List<EnemyBehavior> _enemiesAssigned;
    [SerializeField]
    protected PlayerBehavior _player;
    private float _sqrFightDistance;
    private System.Diagnostics.Stopwatch timer;

    protected virtual void Awake()
    {
        _sqrFightDistance = FightDistance * FightDistance;
        timer = new System.Diagnostics.Stopwatch();
    }

    private void Update()
    {
        if (_player == null)
            return;
        
        PlayerInWarningZone = ContainsInZone(_player.transform);
        PlayerInDangerZone = ContainsInDangerZone(_player.transform);

        AssignEnemiesToFight();
    }

    private void AssignEnemiesToFight()
    {
        var distances = new List<float>();
        var closeEnemies = new List<int>();
        var enemiesFighting = 0;

        _enemiesAssigned = SortEnemiesByDistanceToTarget(_player.transform.position);

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

    protected abstract void OnValidate();

    public abstract Vector3 GetBorder(Transform source, Transform target);

    public abstract bool ContainsInDangerZone(Transform t);

    public abstract bool ContainsInWarningZone(Transform t);

    public abstract bool ContainsInZone(Transform t);

    public void AddEnemy(EnemyBehavior enemy)
    {
        if (!_enemiesAssigned.Contains(enemy))
            _enemiesAssigned.Add(enemy);
    }

    public void AddPlayer(PlayerBehavior player)
    {
        _player = player;
    }

    public void RemoveEnemy(EnemyBehavior enemy)
    {
        _enemiesAssigned.Remove(enemy);
    }

    public void RemovePlayer()
    {
        _player = null;
    }

    public void NotifyEnemies(WarriorBehavior target)
    {
        foreach (var enemy in _enemiesAssigned)
        {
            enemy.SetTarget(target);
        }
    }
}
