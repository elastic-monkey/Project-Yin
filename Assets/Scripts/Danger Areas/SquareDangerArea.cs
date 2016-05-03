using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class SquareDangerArea : DangerArea
{
    public float DangerWidth = 10f;
    public float DangerDepth = 10f;
    public float WarningWidth = 15f;
    public float WarningDepth = 15f;

    private BoxCollider _collider;

    public BoxCollider BoundsCollider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider>();

            return _collider;
        }
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawSquareArena(transform.position, transform.rotation, DangerWidth, DangerDepth, DangerColor);
        GizmosHelper.DrawSquareArena(transform.position, transform.rotation, WarningWidth, WarningDepth, WarningColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player.ToString()))
        {
            var player = other.GetComponentInParent<PlayerBehavior>();
            if (player.Exists())
            {
                _player = player;
                NotifyEnemies(player);
            }
        }
        else if (other.CompareTag(Tags.Enemy.ToString()))
        {
            var enemy = other.GetComponentInParent<EnemyBehavior>();
            if (enemy.Exists())
            {
                AddEnemy(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player.ToString()))
        {
            var player = other.GetComponentInParent<PlayerBehavior>();
            if (player.Exists() && player == _player)
            {
                _player = null;
                NotifyEnemies(null);
            }
        }
        else if (other.CompareTag(Tags.Enemy.ToString()))
        {
            var enemy = other.GetComponentInParent<EnemyBehavior>();
            if (enemy.Exists())
            {
//                RemoveEnemy();
            }
        }
    }

    protected override void OnValidate()
    {
        BoundsCollider.size = new Vector3(WarningWidth, Mathf.Max(WarningWidth, WarningDepth), WarningDepth);
    }

    public override Vector3 GetBorder(Transform source, Transform target)
    {
        throw new System.NotImplementedException();
    }

    public override bool ContainsInDangerRadius(Transform t)
    {
        if (_player.IsNull())
            return false;
        else
            return _player.transform == t;
    }

    public override bool ContainsInWarningRadius(Transform t)
    {
        if (_player.IsNull())
            return false;
        else
            return _player.transform == t;
    }
}
