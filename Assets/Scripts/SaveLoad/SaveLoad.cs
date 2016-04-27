using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	private static string _savePath = "Savegame";

	public static void Save(){
		int saveSlot = GetCurrentSaveSlot ();
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

	public static GameState Load (){
		int saveSlot = GetCurrentSaveSlot ();
		if (File.Exists (_savePath + saveSlot.ToString())) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (_savePath + saveSlot.ToString(), FileMode.Open);
			GameState state = (GameState)bf.Deserialize (file);
			file.Close ();
			return state; 
		} else {
			return null;
		}
	}

	public static List<GameState> GetAllSaves(){
		List<GameState> saves = new List<GameState> ();
		for (int i = 0; i < 4; i++) {
			if (File.Exists (_savePath + i.ToString ())) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (_savePath + i.ToString(), FileMode.Open);
				GameState state = (GameState)bf.Deserialize (file);
				file.Close ();
				saves.Add (state);
			}
		}
		return saves;
	}

	private static int GetCurrentSaveSlot(){
		if (File.Exists ("SSS.txt")) {
			using (TextReader reader = File.OpenText ("SSS.txt")) {
				return int.Parse (reader.ReadLine ());
			}
		}
		return 0;
	}
}
