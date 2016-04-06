﻿using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour
{
	public int MovementState;
	public int DeadBool;
	public int SpeedFloat;
	public int AttackingBool;

	void Awake ()
	{
		MovementState = Animator.StringToHash("Base Layer.MOVEMENT");
		DeadBool = Animator.StringToHash("Dead");
		SpeedFloat = Animator.StringToHash("Speed");
		AttackingBool = Animator.StringToHash("Attacking");
	}
}