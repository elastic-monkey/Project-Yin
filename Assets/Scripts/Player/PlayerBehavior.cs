using UnityEngine;

[RequireComponent(typeof(AttackBehavior), typeof(Animator), typeof(PlayerMovement))]
public class PlayerBehavior : MonoBehaviour
{
    private AttackBehavior _attackBehavior;
    private PlayerMovement _playerMovement;
    private PlayerInput _input;
    private Animator _animator;

    private bool _oldMoving;

    void Awake()
    {
        _attackBehavior = GetComponent<AttackBehavior>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _input = new PlayerInput();
    }

    void Update()
    {
        HandleInput();

        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimatorHashIDs.CanMoveBool, _playerMovement.CanMove);
        _animator.SetFloat(AnimatorHashIDs.SpeedFloat, _playerMovement.CurrentSpeed);
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_input);
    }

    private void HandleInput()
    {
        _input.Receive();
    }
}

[System.Serializable]
public class PlayerInput
{
    public float HorizontalAxis = 0f, VerticalAxis = 0f;
    public bool Fire1 = false, Fire2 = false;

    public void Receive()
    {
        HorizontalAxis = Input.GetAxis(Axis.Horizontal);
        VerticalAxis = Input.GetAxis(Axis.Vertical);
        Fire1 = Input.GetButtonDown(Axis.Fire1);
        Fire2 = Input.GetButtonDown(Axis.Fire2);
    }
}
