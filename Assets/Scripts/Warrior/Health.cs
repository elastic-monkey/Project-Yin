using UnityEngine;
using UnityEngine.UI;

public class Health : Upgradable
{
    public float HealthLevelIncrement = 20f;
    public float BaseMaxHealth = 100f;
    public float MaxHealth = 100f;
    public Slider HealthSlider;
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
            if (HealthSlider != null)
            {
                HealthSlider.value = _currentHealth;
            }
        }
    }

    private void Awake()
    {
        if (HealthSlider == null)
        {
            Debug.LogWarning("HealthSlider is null. No exception will be thrown, but this must be repaired.");
        }
        else
        {
            HealthSlider.minValue = 0;
            HealthSlider.maxValue = MaxHealth;
        }

        CurrentHealth = MaxHealth;
        Alive = true;
    }

    public void RegenerateFull()
    {
        CurrentHealth = MaxHealth;
        Alive = true;
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
