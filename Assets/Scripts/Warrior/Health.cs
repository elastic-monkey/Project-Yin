using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
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
            if (HealthSlider.Exists())
            {
                HealthSlider.value = _currentHealth;
            }
        }
    }

    private void Awake()
    {
        //TODO LOAD HEALTH FROM SAVEFILE
        CurrentHealth = MaxHealth;
        if (HealthSlider.IsNull())
        {
            Debug.LogWarning("HealthSlider is null. No exception will be thrown, but this must be repaired.");
        }
        Alive = true;
    }

    public void RegenerateFull()
    {
        CurrentHealth = MaxHealth;
        Alive = true;
    }
}
