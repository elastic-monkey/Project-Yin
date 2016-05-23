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
            if (_currentExp + value > MaxExperience)
            { // LEVEL UP
                SkillPoints++;
                _currentExp = _currentExp + value - MaxExperience;
            }
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
        CurrentExperience = 0f;
    }

    public void GiveExp(float exp)
    {
        CurrentExperience += exp;
    }

	public void ConsumeSkillPoints(int sp)
	{
		SkillPoints -= sp;
	}
}
