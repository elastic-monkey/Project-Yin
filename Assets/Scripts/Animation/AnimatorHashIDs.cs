using UnityEngine;

public static class AnimatorHashIDs
{
	public static int MovementState = Animator.StringToHash("Base Layer.MOVEMENT");
    public static int DeadBool = Animator.StringToHash("Dead");
	public static int SpeedFloat = Animator.StringToHash("Speed");
    public static int AttackingBool = Animator.StringToHash("Attacking");
	public static int DefendingBool = Animator.StringToHash("Defending");
	public static int SpeedMultiFloat = Animator.StringToHash("SpeedMulti");
	public static int SuddenShiftBool = Animator.StringToHash("SuddenShift");
    public static int CanMoveBool = Animator.StringToHash("CanMove");
    public static int MovingBool = Animator.StringToHash("Moving");
}