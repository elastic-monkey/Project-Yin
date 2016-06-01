using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public Animator Animator;
    public bool Active = false;
    public bool Engaged = false;
    public bool Talking = false;
    [Range(0, 5)]
    public float MaxBeginDelay = 5f;
    [Range(0, 1)]
    public float TalkProbability = 0.5f;
    public float TalkDuration = 5f;
    [Range(0, 1)]
    public float EngagedProbability = 0.5f;
    public float EngagedDuration = 5f;

    private float _currentInteractionTime;
    private float _currentCheckTime;
    private float _startTime;

    private void Start()
    {
        _currentInteractionTime = 0f;
        _currentCheckTime = 0f;
        _startTime = Random.Range(0, MaxBeginDelay);
    }

    public void Update()
    {
        _currentInteractionTime += Time.deltaTime;
        _currentCheckTime += Time.deltaTime;

        if (Talking)
        {
            if (_currentInteractionTime >= TalkDuration)
            {
                Talk(false);
            }
        }
        else if (Engaged)
        {
            if (_currentInteractionTime >= EngagedDuration)
            {
                Engage(false);
            }
        }
        else if (Active)
        {
            if (_currentCheckTime >= 1f)
            {
                _currentCheckTime = 0;

                var rand = Random.Range(0f, 1f);
                if (rand < TalkProbability)
                {
                    if (rand >= EngagedProbability)
                    {
                        Talk(true);
                    }
                    else if (Random.Range(0, 1) == 0)
                    {
                        Engage(true);
                    }
                }
                else if (rand < EngagedProbability)
                {
                    Engage(true);
                }
            }
        }
        else if(_currentCheckTime > _startTime)
        {
            _currentCheckTime = 0;
            Active = true;
        }

        Animator.SetBool(AnimatorHashIDs.NPCTalkingBool, Talking);
        Animator.SetBool(AnimatorHashIDs.NPCEngagedBool, Engaged);
    }

    private void Talk(bool value)
    {
        if (value)
        {
            Talking = true;           
            _currentInteractionTime = 0;
        }
        else
        {
            Talking = false;
            _currentCheckTime = 0;
        }
    }

    private void Engage(bool value)
    {
        if (value)
        {
            Engaged = true;
            _currentInteractionTime = 0;
        }
        else
        {
            Engaged = false;
            _currentCheckTime = 0;
        }
    }
}
