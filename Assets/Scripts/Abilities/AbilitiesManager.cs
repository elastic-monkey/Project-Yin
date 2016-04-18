using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerBehavior))]
public class AbilitiesManager : MonoBehaviour
{
	private PlayerBehavior _player;
	[SerializeField]
	private List<Ability> _abilities;

	public List<Ability> Abilities
	{
		get
		{
			if (_abilities == null)
			{
				_abilities = new List<Ability>();
				LoadAbilities();
			}

			return _abilities;
		}
	}

	void Awake()
	{
		_player = GetComponent<PlayerBehavior>();
	}

	public void ApplyAbilities()
	{
		var selectedAbility = -1;
		for (var i = 0; i < Abilities.Count; i++)
		{
			if (PlayerInput.IsButtonDown(Abilities[i].InputAxis))
			{
				selectedAbility = i;
			}
		}
		if (selectedAbility != -1)
		{
			for (var i = 0; i < Abilities.Count; i++)
			{
				var ability = Abilities[i];
				if (i == selectedAbility)
				{
					ability.Activate(_player);
				}
				else
				{
					ability.Deactivate(_player);
				}
			}
		}
	}

	private void LoadAbilities()
	{
		// TODO: load from file
		_abilities.Clear();

		_abilities.Add(new VisionAbility(Axis.Ability1));
		_abilities.Add(new SpeedAbility(Axis.Ability2));
		_abilities.Add(new ShieldAbility(Axis.Ability3));
		_abilities.Add(new StrengthAbility(Axis.Ability4));
	}
}