using UnityEngine;

public static class AnimatorHashIDs
{
    // Warrior
    public static int DeadBool = Animator.StringToHash("Dead");
    public static int SpeedFloat = Animator.StringToHash("Speed");
    public static int SpeedMultiplierFloat = Animator.StringToHash("Speed Multiplier");
    public static int AttackingBool = Animator.StringToHash("Attacking");
    public static int DefendingBool = Animator.StringToHash("Defending");
    public static int DodgingBool = Animator.StringToHash("Dodging");
    public static int CanMoveBool = Animator.StringToHash("CanMove");
    public static int MovingBool = Animator.StringToHash("Moving");
    public static int AttackMultiplierFloat = Animator.StringToHash("Attack Multiplier");

    // NPCs
    public static int NPCTalkingBool = Animator.StringToHash("Talking");
    public static int NPCEngagedBool = Animator.StringToHash("Engaged");

    // Store
    public static int StoreOpenTrigger = Animator.StringToHash("Open");
    public static int StoreCloseTrigger = Animator.StringToHash("Close");
}