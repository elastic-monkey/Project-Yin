using UnityEngine;
using UnityEngine.UI;

public class Stamina : Upgradable
{
    public float StaminaLevelIncrement = 20f;
    public float BaseMaxStamina = 100f;
    public float MaxStamina = 100f;
    public float RegenerationRate = 10f;
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
        }
    }

    void Awake()
    {
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
        MaxStamina = GetMaxStamina(level);
    }

    private float GetMaxStamina(int level)
    {
        return BaseMaxStamina + level * StaminaLevelIncrement;
    }

    protected override bool OnCanBeUpgradedTo(int level)
    {
        return true;
    }

    public override string GetFlavorText()
    {
        return "Combat experience allows Yin to better measure her blows, better managing her stamina.";
    }

    public override string GetEffectText(int level)
    {
        return "Increases Energy to " + GetMaxStamina(level).ToString();
    }
}
