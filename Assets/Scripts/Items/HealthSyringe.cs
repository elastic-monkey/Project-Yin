using UnityEngine;
using System.Collections;

[System.Serializable]
public class HealthSyringe : Item
{

    public int RecoveryValue;

    public HealthSyringe()
    {
        Type = ItemType.HealthRecovery;
    }

    public override void UseItem(PlayerBehavior player)
    {
        player.Health.RecoverHealth(RecoveryValue);
    }
}
