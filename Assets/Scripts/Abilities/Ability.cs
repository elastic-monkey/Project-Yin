using UnityEngine;
using System.Collections;

public class Ability {

	private const int MaxLevel = 4;
			
	public int Id;
	public int Level;
	public bool Active;

	public bool CanUpgrade{
		get{
			return Level < MaxLevel;
		}
	}
}
