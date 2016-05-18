using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class HealthSyringe : Item
{
 
    public int RecoveryValue;

    public void Start()
    {
        Icon = GetComponent<Image>();
    }

    public HealthSyringe()
    {
        Type = ItemType.HealthRecovery;
    }

    public override void UseItem()
    {
        Player.Health.RecoverHealth(RecoveryValue);
    }

    public override bool CanUse()
    {
        if (Player.Health.CurrentHealth == Player.Health.MaxHealth)
            return false;
        return true;
    }
}
