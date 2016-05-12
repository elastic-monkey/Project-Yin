using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class GameState
{
	public int CurrentSlot;
    public string CurrentScene;
    public PlayerState PlayerState;
    public CameraState CameraState;

    public GameState()
    {
		CurrentSlot = SaveLoad.GetCurrentSaveSlot ();
        CurrentScene = SceneManager.GetActiveScene().name;
        PlayerState = new PlayerState();
        CameraState = new CameraState();
    }
}
