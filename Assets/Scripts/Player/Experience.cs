using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    public float MaxExperience = 100f;
    public int SkillPoints;

    private float _currentExp;

    public float CurrentExperience
    {
        get
        {
            return _currentExp;
        }
        set
        {
            _currentExp = value;
        }
    }

    public bool HasSkillPoints
    {
        get
        {
            return SkillPoints > 0;
        }
    }

    private void Awake()
    {
        _currentExp = 0f;
    }

    public void GiveExp(float exp)
    {
        if (_currentExp + exp > MaxExperience)
        { // LEVEL UP
            SkillPoints++;
            _currentExp = _currentExp + (exp - MaxExperience);
        }
        else
        {
            _currentExp += exp;
        }
    }

	public void ConsumeSkillPoints(int sp)
	{
		SkillPoints -= sp;
	}
}
