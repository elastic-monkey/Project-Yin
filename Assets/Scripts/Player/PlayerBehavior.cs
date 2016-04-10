using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerBehavior : WarriorBehavior
{
    private PlayerMovement _playerMovement;
    [SerializeField]
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

        _input.Receive();

        _attackBehavior.ApplyAttack(_input);

        if (!_attackBehavior.Attacking)
            _defenseBehavior.ApplyDefense(_input);

        _playerMovement.CanMove = !_attackBehavior.Attacking && !_defenseBehavior.Defending;

        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimatorHashIDs.CanMoveBool, _playerMovement.CanMove);
        //_animator.SetFloat(AnimatorHashIDs.SpeedFloat, _playerMovement.CurrentSpeed);
		if (_playerMovement.Moving) {
			_animator.SetFloat (AnimatorHashIDs.SpeedFloat, _playerMovement.SpeedThreshold, _playerMovement.SpeedDampTime, Time.deltaTime);
		} else {
			_animator.SetFloat (AnimatorHashIDs.SpeedFloat, 0f);
		}
		_animator.SetBool(AnimatorHashIDs.AttackingBool, _attackBehavior.Attacking);
		_animator.SetBool (AnimatorHashIDs.DefendingBool, _defenseBehavior.Defending);
    }

    void FixedUpdate()
    {
        _playerMovement.ApplyMovement(_input);
    }
}

[System.Serializable]
public class PlayerInput
{
    public float HorizontalAxis = 0f, VerticalAxis = 0f;
    public bool Fire1 = false, Fire2 = false, Fire3Down = false, Fire3Up = false;

    public void Receive()
    {
        HorizontalAxis = Input.GetAxis(Axis.Horizontal);
        VerticalAxis = Input.GetAxis(Axis.Vertical);
        Fire2 = Input.GetButtonDown(Axis.Fire2);
        Fire3Down = Input.GetButtonDown(Axis.Fire3);
        Fire3Up = Input.GetButtonUp(Axis.Fire3);
    }
}
