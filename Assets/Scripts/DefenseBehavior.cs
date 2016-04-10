using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health), typeof(Stamina))]
public class DefenseBehavior : MonoBehaviour
{
    public Tags EnemyTag;
    public Defense[] Defenses;
    public bool DefenseIsOn = true;
    public bool Defending = false;

    private Health _health;
    private Stamina _stamina;
    private int _currentDefense;
    private bool _cancelDefense;
    

    public int CurrentDefense
    {
        get
        {
            return _currentDefense;
        }

        set
        {
            _currentDefense = Mathf.Clamp(value, 0, Defenses.Length - 1);
        }
    }

    public bool CanDefend
    {
        get
        {
            return DefenseIsOn && !Defending;
        }
    }

    void Awake()
    {
        _health = GetComponent<Health>();
        _stamina = GetComponent<Stamina>();
    }

    void Start()
    {
        CurrentDefense = 0;
    }

    public void TakeDamage(int damage)
    {
        if (Defending)
        {
            var actualDamage = damage - Defenses[CurrentDefense].Armour;
            _health.CurrentHealth -= actualDamage;
        }
        else
        {
            _health.CurrentHealth -= damage;
        }
    }

    public void ChooseAndApplyDefense()
    {
        var bestArmour = float.MinValue;
        var best = -1;

        for (int i = 0; i < Defenses.Length; i++)
        {
            var attack = Defenses[i];
            if (_stamina.CanConsume(attack.StaminaCost))
            {
                if (attack.Armour > bestArmour)
                {
                    bestArmour = attack.Armour;
                    best = i;
                }
            }
        }

        if (best < 0)
            return;

        ApplyDefense(best);
    }

    public void ApplyDefense(int index)
    {
        if (!CanDefend)
            return;

        CurrentDefense = index;
        _cancelDefense = false;

        var defense = Defenses[CurrentDefense];

        StartCoroutine(DefenseCoroutine(defense));
    }

    public void CancelDefense()
    {
        _cancelDefense = true;
    }

    private IEnumerator DefenseCoroutine(Defense defense)
    {
        Defending = true;

        if (!defense.UseDuration)
        {
            _stamina.RegenerateIsOn = false;
            var defenseDec = defense.StaminaCost * Time.deltaTime;
            while (_stamina.CanConsume(defenseDec) && !_cancelDefense)
            {
                _stamina.ConsumeStamina(defenseDec);
                defenseDec = defense.StaminaCost * Time.deltaTime;
                yield return null;
            }
            _stamina.RegenerateIsOn = true;
        }
        else
        {
            _stamina.ConsumeStamina(defense.StaminaCost);

            yield return new WaitForSeconds(defense.Duration);
        }

        Defending = false;
    }
}

[System.Serializable]
public class Defense
{
    public int Armour;
    public int StaminaCost;
    public bool UseDuration = true;
    public float Duration;
}
