using UnityEngine;
using System.Collections.Generic;

public abstract class DangerArea : MonoBehaviour
{
    public Color DangerColor = Color.red;
    public Color WarningColor = Color.yellow;

    [SerializeField]
    protected List<EnemyBehavior> _enemiesAssigned;
    protected PlayerBehavior _player;

    protected abstract void OnValidate();

    public abstract Vector3 GetBorder(Transform source, Transform target);

    public abstract bool ContainsInDangerRadius(Transform t);

    public abstract bool ContainsInWarningRadius(Transform t);

    public void AddEnemy(EnemyBehavior enemy)
    {
        _enemiesAssigned.Add(enemy);
    }

    public void RemoveEnemy(EnemyBehavior enemy)
    {
        _enemiesAssigned.Remove(enemy);
    }

    protected void NotifyEnemies(PlayerBehavior player)
    {
        foreach (var enemy in _enemiesAssigned)
        {
            enemy.Target = player;
        }
    }
}
