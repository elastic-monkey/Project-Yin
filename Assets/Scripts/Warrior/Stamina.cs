using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
	public static int MaxLevel = 4;

    public float MaxStamina = 100f;
    public float RegenerationRate = 10f;
	public int CurrentLevel;
	public Slider StaminaSlider;
    public bool RegenerateIsOn = true;
    public bool Regenerating = false;

    private float _currentStamina;
	private PlayerBehavior _player;

	public bool CanBeUpgraded
	{
		get
		{
			return CurrentLevel < MaxLevel;
		}
	}

    public float CurrentStamina
    {
        get
        {
            return _currentStamina;
        }

        set
        {
            _currentStamina = Mathf.Clamp(value, 0, MaxStamina);
            if (StaminaSlider.Exists())
            {
                StaminaSlider.value = _currentStamina;
            }
        }
    }

    void Awake()
    {
		_player = GetComponent<PlayerBehavior> ();
        CurrentStamina = MaxStamina;
        if (StaminaSlider.IsNull())
        {
            Debug.LogWarning("StaminaSlider is null. No exception will be thrown, but this must be repaired.");
        }
    }

    void Update()
    {
        if (RegenerateIsOn && Regenerating)
        {
            CurrentStamina += RegenerationRate * Time.deltaTime;
            Regenerating = Regenerating && CurrentStamina < MaxStamina;
        }
    }

    public void ConsumeStamina(float stamina)
    {
        CurrentStamina -= stamina;
        Regenerating = true;
    }

    public bool CanConsume(float value)
    {
        return CurrentStamina >= value;
    }

	public void Upgrade()
	{
		if (CanBeUpgraded && _player.Experience.SkillPoints >= 1)
		{
			CurrentLevel++;
			MaxStamina += 20;
			_player.Experience.ConsumeSkillPoints (1);
		} else {
			Debug.Log ("Stamina Cannot be Upgraded any further");
		}
	}
}
