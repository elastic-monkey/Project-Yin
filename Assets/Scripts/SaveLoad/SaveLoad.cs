using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	private static string _savePath = "Savegame";

	public static void Save(bool isCheckpoint){
		string saveName = GetSaveName (isCheckpoint);
		GameState savedGame = new GameState();
		FileStream file;
		BinaryFormatter bf = new BinaryFormatter ();
		if (!File.Exists (saveName)) {
			file = File.Create (saveName);
		} else {
			file = File.Open (saveName, FileMode.Open, FileAccess.Write);
		}
		bf.Serialize (file, savedGame);
		file.Close ();
	}

	public static GameState Load (bool isCheckpoint){
		string saveName = GetSaveName (isCheckpoint);
		if (File.Exists (saveName)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (saveName, FileMode.Open);
			GameState state = (GameState)bf.Deserialize (file);
			file.Close ();
			return state; 
		} else {
			return null;
		}
	}

	public static GameState LoadCheckpoint(){
		if (File.Exists ("Checkpoint")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open ("Checkpoint", FileMode.Open);
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

	private static string GetSaveName(bool check){
		string saveName;
		if (check) {
			saveName = "Checkpoint";
		} else {
			saveName = _savePath + GetCurrentSaveSlot ().ToString ();
		}
		return saveName;
	}
}
