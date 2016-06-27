using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Utilities;

public static class SaveLoad
{
    private static string _savePath = "Savegame";
    private static string _audioSettingsSavePath = "audio_settings.sav";

    public static void Save(bool isCheckpoint)
    {
        string saveName = GetSaveName(isCheckpoint);
        var savedGame = new GameState();
        Stream file;
        if (!File.Exists(saveName))
        {
            file = File.Create(saveName);
        }
        else
        {
            file = File.OpenWrite(saveName);
        }
        var bf = new BinaryFormatter();
        bf.Serialize(file, savedGame);
        file.Close();
    }

    public static void SaveAudioSettings()
    {
        Debug.Log("Save audio settings");
        IOHelper.SerializeToFile(_audioSettingsSavePath, GameAudio.Serialize());
    }

    public static void LoadAudioSettings()
    {
        Debug.Log("Load audio settings");
        SerializableAudioSettings loadSettings;
        if (IOHelper.DeserializeFromFile<SerializableAudioSettings>(_audioSettingsSavePath, out loadSettings))
        {
            GameAudio.LoadFromSerialized(loadSettings);
        }
        else
        {
            SaveAudioSettings();
        }
    }

    public static GameState Load(bool isCheckpoint)
    {
        string saveName = GetSaveName(isCheckpoint);
        if (File.Exists(saveName))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(saveName, FileMode.Open);
            var state = (GameState)bf.Deserialize(file);
            file.Close();
            return state; 
        }
        else
        {
            return null;
        }
    }

    public static GameState LoadCheckpoint()
    {
        if (File.Exists("Checkpoint"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("Checkpoint", FileMode.Open);
            GameState state = (GameState)bf.Deserialize(file);
            file.Close();
            return state; 
        }
        else
        {
            return null;
        }
    }

    public static List<GameState> GetAllSavedGames()
    {
        List<GameState> saves = new List<GameState>();
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(_savePath + i.ToString()))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(_savePath + i.ToString(), FileMode.Open);
                GameState state = (GameState)bf.Deserialize(file);
                file.Close();
                saves.Add(state);
            }
        }
        return saves;
    }

    public static int GetCurrentSaveSlot()
    {
        if (File.Exists("SSS.txt"))
        {
            using (TextReader reader = File.OpenText("SSS.txt"))
            {
                return int.Parse(reader.ReadLine());
            }
        }
        return 0;
    }

    private static string GetSaveName(bool check)
    {
        if (check)
            return "Checkpoint";
        else
            return _savePath + GetCurrentSaveSlot().ToString();
    }

	public static void DeleteSaveGame(int slot){
		if (File.Exists (_savePath + slot.ToString ())) {
			File.Delete (_savePath + slot.ToString ());
		}
	}

	public static void DeleteCheckpoint(){
		if (File.Exists ("Checkpoint")) {
			File.Delete ("Checkpoint");
		}
	}
}
