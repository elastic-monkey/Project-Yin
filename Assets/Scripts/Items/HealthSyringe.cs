using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class HealthSyringe : Item
{
    public int RecoveryValue;

    public void Start()
    {
        Type = ItemType.HealthRecovery;
        Effect = "Restores " + RecoveryValue.ToString() + " Health Points";
        FlavorText = "A small syringe containing a red liquid";
    }

    public override void UseItem()
    {
        _player.Health.RecoverHealth(RecoveryValue);
    }

    public override bool CanUse()
    {
        if (_player.Health.CurrentHealth == _player.Health.MaxHealth)
            return false;
        
        return true;
    }

    private void OnValidate()
    {
        Type = ItemType.HealthRecovery;
        Effect = "Restores " + RecoveryValue.ToString() + " Health Points";
        FlavorText = "A small syringe containing a red liquid";
    }
}
