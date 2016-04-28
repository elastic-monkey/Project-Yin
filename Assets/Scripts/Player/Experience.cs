using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour {

	public float MaxExperience = 100f;
	public Slider ExpSlider;
	public Image AvailableSP;
	public int SkillPoints;

	private float _currentExp;

	public float CurrentExp{
		get{
			return _currentExp;
		}
		set{
			if (_currentExp + value > MaxExperience) { // LEVEL UP
				SkillPoints++;
				_currentExp = _currentExp + value - MaxExperience;
			}
			ExpSlider.value = _currentExp;
		}
	}

	public bool HasSkillPoints{
		get{
			return SkillPoints > 0;
		}
	}

	void Awake(){
		CurrentExp = 0f;
	}

	void Update(){
		if (HasSkillPoints && !AvailableSP.enabled) {
			AvailableSP.enabled = true;
		} else if (!HasSkillPoints && AvailableSP.enabled) {
			AvailableSP.enabled = false;
		}
	}

	public void GiveExp(float exp){
		CurrentExp += exp;
	}
}
