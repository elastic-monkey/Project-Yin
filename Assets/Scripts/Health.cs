using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    public Slider HealthSlider;

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
            HealthSlider.value = _currentHealth;
        }
    }

    public bool IsDead
    {
        get
        {
            return CurrentHealth <= 0;
        }
    }

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }
}
