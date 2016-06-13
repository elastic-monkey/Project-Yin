using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health), typeof(Stamina))]
public class DefenseBehavior : MonoBehaviour
{
	public Defense[] Defenses;
	public bool CanDefend = true;
	public bool Defending = false;
	public bool ShieldOn = false;

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

	public bool CanPerformNewDefense
	{
		get
		{
			return CanDefend && !Defending;
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

	public void TakeDamage(float damage)
	{
		if (!ShieldOn)
		{
			if (Defending)
			{
                var actualDamage = Mathf.Max(damage - Defenses[CurrentDefense].Armour, 0f);
				_health.CurrentHealth -= actualDamage;
                _stamina.ConsumeStamina(Defenses[CurrentDefense].StaminaCost);
			}
			else
			{
				_health.CurrentHealth -= damage;
			}
		}
		else
		{
			ShieldOn = false;
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

	public void ApplyDefense()
	{
		var chosenDefenseIndex = -1;

        if (PlayerInput.IsButtonDown(Axes.Defend))
        {
            chosenDefenseIndex = 0;
        }
        else if (PlayerInput.IsButtonUp(Axes.Defend))
        {
            CancelDefense();
        }

		ApplyDefense(chosenDefenseIndex);
	}

	private void ApplyDefense(int index)
	{
		if (!CanPerformNewDefense)
			return;

		if (index < 0 || index >= Defenses.Length)
			return;

		CurrentDefense = index;
		_cancelDefense = false;

		var defense = Defenses[CurrentDefense];

		StartCoroutine(DefenseCoroutine(defense));
	}

	public void CancelDefense()
	{
		_cancelDefense = true;
        _stamina.RegenerateIsOn = true;
	}

	private IEnumerator DefenseCoroutine(Defense defense)
	{
		Defending = true;

		if (!defense.OneTime)
		{
			_stamina.RegenerateIsOn = false;
			var defenseDec = defense.StaminaCost * Time.deltaTime;
			while (_stamina.CanConsume(defenseDec) && !_cancelDefense)
			{
				//_stamina.ConsumeStamina(defenseDec);
				//defenseDec = defense.StaminaCost * Time.deltaTime;
				yield return null;
			}
			_stamina.RegenerateIsOn = true;
		}
		else
		{
			//_stamina.ConsumeStamina(defense.StaminaCost);

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
	public bool OneTime = true;
	public float Duration;
}
