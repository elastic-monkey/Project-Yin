﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	private static string _savePath = "Savegame";

	public static void Save(int saveSlot){
		GameState savedGame = new GameState();
		FileStream file;
		BinaryFormatter bf = new BinaryFormatter ();
		if (!File.Exists (_savePath + saveSlot.ToString())) {
			file = File.Create (_savePath + saveSlot.ToString());
			bf.Serialize (file, savedGame);
			file.Close ();
		} else {
			file = File.Open (_savePath + saveSlot.ToString(), FileMode.Open, FileAccess.Write);
			bf.Serialize (file, savedGame);
			file.Close ();
		}
	}

	public static GameState Load (int saveSlot){
		if (File.Exists (_savePath + saveSlot.ToString())) {
			Debug.Log ("FOUND SAVES");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (_savePath + saveSlot.ToString(), FileMode.Open);
			GameState state = (GameState)bf.Deserialize (file);
			file.Close ();
			return state; 
		} else {
			Debug.Log ("DID NOT FIND SAVE GAME");
			return null;
		}
	}
}
