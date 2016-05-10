using UnityEngine;
using UnityEngine.UI;

public class Stamina : Upgradable
{
    public float StaminaLevelIncrement = 20f;
    public float BaseMaxStamina = 100f;
    public float MaxStamina = 100f;
    public float RegenerationRate = 10f;
	public Slider StaminaSlider;
    public bool RegenerateIsOn = true;
    public bool Regenerating = false;

    private float _currentStamina;

    public float CurrentStamina
    {
        get
        {
            return _currentStamina;
        }

        set
        {
            _currentStamina = Mathf.Clamp(value, 0, MaxStamina);
            if (StaminaSlider != null)
            {
                StaminaSlider.value = _currentStamina;
            }
        }
    }

    void Awake()
    {
        if (StaminaSlider == null)
        {
            Debug.LogWarning("StaminaSlider is null. No exception will be thrown, but this must be repaired.");
        }

        CurrentStamina = MaxStamina;
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

    protected override void OnUpgradeTo(int level)
    {
        MaxStamina = BaseMaxStamina + level * StaminaLevelIncrement;
    }

    protected override bool OnCanBeUpgradedTo(int level)
    {
        return true;
    }
}
