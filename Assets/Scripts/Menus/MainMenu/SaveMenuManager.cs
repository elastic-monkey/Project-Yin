using UnityEngine;
using System.Collections.Generic;

public class SaveMenuManager : MonoBehaviour {

	public static List<GameState> saves;

	void Awake () {
		saves = SaveLoad.GetAllSavedGames ();
		Debug.Log ("GOT " + saves.Count);
	}
}
