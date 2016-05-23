using UnityEngine;
using UnityEngine.UI;

public class Health : Upgradable
{
    public float HealthLevelIncrement = 20f;
    public float BaseMaxHealth = 100f;
    public float MaxHealth = 100f;
    public bool Alive;

    [SerializeField]
    private float _currentHealth;

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
        }
    }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        Alive = true;
    }

    public void RegenerateFull()
    {
        CurrentHealth = MaxHealth;
        Alive = true;
    }

	public void RecoverHealth(int recovery){
		if (CurrentHealth + recovery > MaxHealth) {
			CurrentHealth = MaxHealth;
		} else {
			CurrentHealth += recovery;
		}
	}

    protected override void OnUpgradeTo(int level)
    {
        MaxHealth = BaseMaxHealth + level * HealthLevelIncrement;
        RegenerateFull();
    }

    protected override bool OnCanBeUpgradedTo(int level)
    {
        return true;
    }
}
