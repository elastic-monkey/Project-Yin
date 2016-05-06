using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{

    public float MaxExperience = 100f;
    public Slider ExpSlider;
    public Image AvailableSP;
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
            if (ExpSlider.Exists())
            {
                ExpSlider.value = _currentExp;
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

    void Awake()
    {
        CurrentExperience = 0f;
        if (ExpSlider.IsNull())
        {
            Debug.LogWarning("ExpSlider is null. No exception will be thrown, but this must be repaired.");
        }

        if (AvailableSP.IsNull())
        {
            Debug.LogWarning("AvailableSP is null. No exception will be thrown, but this must be repaired.");
        }
    }

    void Update()
    {
        if (AvailableSP.Exists())
        {
            if (HasSkillPoints && !AvailableSP.enabled)
            {
                AvailableSP.enabled = true;
            }
            else if (!HasSkillPoints && AvailableSP.enabled)
            {
                AvailableSP.enabled = false;
            }
        }
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
