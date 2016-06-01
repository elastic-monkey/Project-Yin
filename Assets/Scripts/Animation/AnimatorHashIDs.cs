using UnityEngine;

public static class AnimatorHashIDs
{
    // Warrior
    public static int DeadBool = Animator.StringToHash("Dead");
    public static int SpeedFloat = Animator.StringToHash("Speed");
    public static int SpeedMultiplierFloat = Animator.StringToHash("Speed Multiplier");
    public static int AttackingBool = Animator.StringToHash("Attacking");
    public static int DefendingBool = Animator.StringToHash("Defending");
    public static int CanMoveBool = Animator.StringToHash("CanMove");
    public static int MovingBool = Animator.StringToHash("Moving");

    // NPCs
    public static int NPCTalkingBool = Animator.StringToHash("Talking");
    public static int NPCEngagedBool = Animator.StringToHash("Engaged");
}