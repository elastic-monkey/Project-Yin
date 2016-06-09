using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public PlayerBehavior Player;
    public EnemiesManager EnemiesManager;
    public HideBuidings HideBuildings;
    public SwapToFadeManager SwapFadeMaterials;
	public GameOver GameOverMenu;
    public InteractionPrompt InteractionPrompt;

    [SerializeField]
    private bool _gamePaused;
    private List<WarriorBehavior> _players, _enemies;

    public bool GamePaused
    {
        get
        {
            return _gamePaused;
        }

        private set
        {
            _gamePaused = value;
            PlayerInput.OnlyMenus = value;
            Time.timeScale = value ? 0 : 1;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType<GameManager>();
                if (objs.Length == 0)
                    Debug.LogError("There is no instantiated GameManager");
                else
                    _instance = objs[0];
            }

            return _instance;
        }
    }

    private void Awake()
    {
        Random.seed = System.DateTime.Now.Millisecond;

        _instance = this;
    }

    private void Start()
    {
        if (Player == null)
        {
            Debug.LogError("Player instance is not defined");
        }

        _players = new List<WarriorBehavior>();
        _players.Add(Player);

        _enemies = new List<WarriorBehavior>();
        foreach (var enemy in EnemiesManager.Enemies)
            _enemies.Add(enemy);
    }

    public List<WarriorBehavior> GetWarriors(Tags tag)
    {
        switch (tag)
        {
            case Tags.Player:
                return _players;
               
            case Tags.Enemy:
                return _enemies;
        }

        return null;
    }

    public void OnWarriorDeath(WarriorBehavior warrior)
    {
        Debug.Log("On Warrior Death: " + warrior.tag);
        Destroy(warrior.gameObject);

        //switch (warrior.tag)
        //{
        //    case Tags.Player:
        //        OnPlayerDeath(warrior.GetComponent<PlayerBehavior>());
        //        break;

        //    case Tags.Enemy:
        //        OnEnemyDeath(warrior.GetComponent<EnemyBehavior>());
        //        break;
        //}
    }

    public void OnPlayerDeath(PlayerBehavior player)
    {
        // TODO: Activate Game Over Menu
		GameOverMenu.OnGameOver(true);
    }

    public void OnEnemyDeath(EnemyBehavior enemy)
    {
        // TODO: Give player experience for the kill.
    }

    public void SetGamePaused(bool value)
    {
        GamePaused = value;
    }

    public void BlockGameplayInput(bool value)
    {
        PlayerInput.OnlyMenus = value;
    }
}