using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public PlayerBehavior Player;
    public HideBuidings HideBuildings;
	public GameOver GameOverMenu;
    public InteractionPrompt InteractionPrompt;

    [SerializeField]
    private bool _gamePaused;

    public bool GamePaused
    {
        get
        {
            return _gamePaused;
        }

        private set
        {
            _gamePaused = value;
            PlayerInput.GameplayBlocked = value;
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
		/*if (_instance != null)
			Destroy (gameObject);*/

        _instance = this;
    }

    private void Start()
    {
        if (Player == null)
            Debug.LogError("Player instance is not defined");
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
        PlayerInput.GameplayBlocked = value;
    }
}