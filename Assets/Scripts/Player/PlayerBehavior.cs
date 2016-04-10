using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerBehavior : WarriorBehavior
{
    private PlayerMovement _playerMovement;
    private PlayerInput _input;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _input = new PlayerInput();
    }

    protected override void Update()
    {
        base.Update();

        HandleInput();

        _attackBehavior.ApplyAttack(_input);
        _playerMovement.CanMove = !_attackBehavior.Attacking;

        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimatorHashIDs.CanMoveBool, _playerMovement.CanMove);
        _animator.SetFloat(AnimatorHashIDs.SpeedFloat, _playerMovement.CurrentSpeed);
        _animator.SetBool(AnimatorHashIDs.AttackingBool, _attackBehavior.Attacking);
    }

    void FixedUpdate()
    {
        _playerMovement.ApplyMovement(_input);
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
        Fire2 = Input.GetButtonDown(Axis.Fire2);
    }
}
