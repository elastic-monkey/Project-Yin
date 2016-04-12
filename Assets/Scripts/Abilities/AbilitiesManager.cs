using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerBehavior))]
public class AbilitiesManager : MonoBehaviour
{
    private PlayerBehavior _player; 
    [SerializeField]
    private List<Ability> _abilities;

    void Awake()
    {
        _player = GetComponent<PlayerBehavior>();
        _abilities = new List<Ability>();
    }

    void Start()
    {
        LoadAbilities();
    }

    public void ApplyAbilities()
    {
		string abilityType = null;
        foreach(var ability in _abilities)
        {
			if (PlayerInput.IsButtonDown (ability.InputAxis)) {
				Debug.Log (string.Concat ("Activating ", ability.GetAbilityType ().ToString ()));
				ability.Activate (_player);
				abilityType = ability.GetAbilityType().ToString();
			}
        }
		if (abilityType != null) {
			foreach(var ability in _abilities){
				if (ability.GetAbilityType().ToString() != abilityType) {
					Debug.Log (string.Concat ("Deactivating ", ability.GetAbilityType ().ToString ()));
					ability.Deactivate (_player);
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