using UnityEngine;

public class Health : Upgradable
{
    public float HealthLevelIncrement = 20f;
    public float BaseMaxHealth = 100f;
    public bool Alive;

    [SerializeField]
    private float _currentHealth, _maxHealth;

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

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }

        set
        {
            _maxHealth = Mathf.Max(value, 0);
        }
    }

    private void Awake()
    {
        MaxHealth = BaseMaxHealth;
        CurrentHealth = MaxHealth;
        Alive = true;
    }

    public void RegenerateFull()
    {
        CurrentHealth = MaxHealth;
        Alive = true;
    }

    public void RecoverHealth(int recovery)
    {
        if (CurrentHealth + recovery > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
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
