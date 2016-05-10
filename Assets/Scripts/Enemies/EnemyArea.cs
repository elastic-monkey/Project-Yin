using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyArea : MonoBehaviour
{
    public Color DangerColor = Color.red;
    public Color WarningColor = Color.yellow;
    public bool PlayerInWarningZone;
    public bool PlayerInDangerZone;

    [SerializeField]
    protected List<EnemyBehavior> _enemiesAssigned;
    [SerializeField]
    protected PlayerBehavior _player;

    private void Update()
    {
        if (_player != null)
        {
            PlayerInWarningZone = ContainsInZone(_player.transform);
            PlayerInDangerZone = ContainsInDangerZone(_player.transform);
        }
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

    private void NotifyEnemies(WarriorBehavior target)
    {
        foreach (var enemy in _enemiesAssigned)
        {
            enemy.SetTarget(target);
        }
    }
}
