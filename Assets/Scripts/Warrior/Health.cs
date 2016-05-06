using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static int MaxLevel = 4;

    public float MaxHealth = 100f;
    public int CurrentLevel;
    public Slider HealthSlider;
    public bool Alive;

    [SerializeField]
    private float _currentHealth;
    private PlayerBehavior _player;

    public bool CanBeUpgraded
    {
        get
        {
            return CurrentLevel < MaxLevel;
        }
    }

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
        _player = GetComponent<PlayerBehavior>();
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

	public void Upgrade(int level)
    {
		if (CanBeUpgraded && _player.Experience.SkillPoints >= 1 && level == CurrentLevel + 1)
        {
            CurrentLevel++;
            MaxHealth += 20;
            RegenerateFull();
            _player.Experience.ConsumeSkillPoints(1);
        }
        else
        {
            Debug.Log("Health Cannot be Upgraded");
        }
    }
}
