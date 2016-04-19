using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public PlayerBehavior Player;
    public EnemiesManager Enemies;

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

    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
    }

    void Start()
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

    private void OnPlayerDeath(PlayerBehavior player)
    {
        // TODO: Restart from last save point?
    }

    public void OnEnemyDeath(EnemyBehavior enemy)
    {
        // TODO: Give player experience for the kill.
    }
}
