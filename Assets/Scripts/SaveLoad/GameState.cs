using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class GameState
{
    public string CurrentScene;
    public PlayerState PlayerState;
    public CameraState CameraState;

    public GameState()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        PlayerState = new PlayerState();
        CameraState = new CameraState();
    }
}
