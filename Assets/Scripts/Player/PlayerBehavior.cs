using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerBehavior : WarriorBehavior
{
    private PlayerMovement _playerMovement;
    public PlayerInput Input;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        Input = new PlayerInput();
    }

    protected override void Update()
    {
        base.Update();

		Input.Receive ();

		if (!Input.Blocked) {

			_attackBehavior.ApplyAttack (Input);

			if (!_attackBehavior.Attacking)
				_defenseBehavior.ApplyDefense (Input);

			_playerMovement.CanMove = !_attackBehavior.Attacking && !_defenseBehavior.Defending;
		}

		SetAnimatorParameters ();
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimatorHashIDs.CanMoveBool, _playerMovement.CanMove);
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
		if (!Input.Blocked) {
			_playerMovement.ApplyMovement (Input);
		}
    }
}

[System.Serializable]
public class PlayerInput
{
    public float HorizontalAxis = 0f, VerticalAxis = 0f;
    public bool Fire1 = false, Fire2 = false, Fire3Down = false, Fire3Up = false;
	public bool Fire1Up = false, Fire1Down = false;
	public bool VisionMode = false, SpeedMode = false, ShieldMode = false, StrengthMode = false;  
	public bool Blocked;

    public void Receive()
    {
        HorizontalAxis = Input.GetAxis(Axis.Horizontal);
        VerticalAxis = Input.GetAxis(Axis.Vertical);

		Fire1 = Input.GetButton(Axis.Fire1);
		Fire1Up = Input.GetButtonUp (Axis.Fire1);
		Fire1Down = Input.GetButtonDown (Axis.Fire1);

		Fire2 = Input.GetButtonUp(Axis.Fire2);
        
		Fire3Down = Input.GetButtonDown(Axis.Fire3);
        Fire3Up = Input.GetButtonUp(Axis.Fire3);

		VisionMode = Input.GetKeyDown (KeyCode.Alpha1);
		SpeedMode = Input.GetKeyDown (KeyCode.Alpha2);
		ShieldMode = Input.GetKeyDown (KeyCode.Alpha3);
		StrengthMode = Input.GetKeyDown (KeyCode.Alpha4);
    }
}
