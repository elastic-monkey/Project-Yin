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

	public void Upgrade(){
		if (CanBeUpgraded) {
			CurrentLevel++;
			MaxHealth += 20;
		} else {
			Debug.Log ("Health Cannot be Upgraded any further");
		}
	}
}
