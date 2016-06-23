using UnityEngine;
using System.Collections;

public class DodgeBehaviour : MonoBehaviour
{

    public bool CanDodge;
    public bool Dodging;
    public float StaminaComsumption = 10.0f;
    public float DodgeDistance = 2.0f;
    public float DodgeDuration = 0.3f;

    private Stamina _stamina;
    private WarriorBehavior _warrior;
    private Rigidbody _rigidBody;
    private float _elapsedTime;

    void Awake()
    {
        _stamina = GetComponent<Stamina>();
        _warrior = GetComponent<WarriorBehavior>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void ApplyDodge()
    {
        if (PlayerInput.IsButtonDown(Axes.Dodge) && !Dodging)
        {
            StartCoroutine(DodgeCoroutine());   
        }
    }

    private IEnumerator DodgeCoroutine()
    {
        if (_stamina.CanConsume(StaminaComsumption))
        {
            _stamina.ConsumeStamina(StaminaComsumption);
            _elapsedTime = 0.0f;

            Dodging = true;
            BlockOtherActions(true);

            Vector3 position = _rigidBody.transform.position;
            Vector3 destination = position - _rigidBody.transform.forward.normalized * DodgeDistance;

            while (_elapsedTime < DodgeDuration)
            {
                _elapsedTime += Time.deltaTime;

                _rigidBody.position = Vector3.Lerp(position, destination, (_elapsedTime / DodgeDuration));

                yield return new WaitForEndOfFrame();
            }
                
            BlockOtherActions(false);
            Dodging = false;
        }
    }

    private void BlockOtherActions(bool value)
    {
        _warrior.Attack.CanAttack = !value;
        _warrior.Defense.CanDefend = !value;
        _stamina.RegenerateIsOn = !value;
    }
}
