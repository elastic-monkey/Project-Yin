using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float MaxStamina = 100f;
    public float RegenerationRate = 10f;
    public Slider StaminaSlider;
    public bool Regenerating;

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
            StaminaSlider.value = _currentStamina;
        }
    }

    void Awake()
    {
        CurrentStamina = MaxStamina;
        Regenerating = true;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Regenerating)
        {
            CurrentStamina += RegenerationRate * Time.deltaTime;
        }

        Regenerating = Regenerating && CurrentStamina < MaxStamina;
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
}
