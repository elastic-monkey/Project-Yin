﻿using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public PlayerBehavior Player;
    public ItemRepo ItemRepo;
    public EnemiesManager EnemiesManager;
    public HideBuidings HideBuildings;
    public SwapToFadeManager SwapFadeMaterials;
    public InteractionPrompt InteractionPrompt;
    public DialogueWindow DialogueWindow;
    public GameMenu SaveTerminal;
    public GameMenu GameOverMenu;
    public StoreMenu StoreMenu;
	public SoundtrackManager SoundtrackManager;
	public MenuSoundManager MenuSoundManager;
	public SoundManager EnemiesSoundManager;
	public PlayerHit SplashHit;

    [SerializeField]
    private bool _gamePaused;
	private bool _gameNotPausedNextFrame;
	private List<WarriorBehavior> _players, _enemies;

    public bool IsGamePaused
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

        SaveLoad.LoadAudioSettings();
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

    private void LateUpdate()
    {
        if (_gameNotPausedNextFrame)
        {
            _gameNotPausedNextFrame = false;
            IsGamePaused = false;
        }
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
        //Debug.Log("On Warrior Death: " + warrior.tag);
        Destroy(warrior.gameObject);
    }

    public void OnPlayerDeath(PlayerBehavior player)
    {
		SetGamePaused(true);
		BlockGameplayInput(true);
		GameOverMenu.Open();
	}

    public void OnEnemyDeath(EnemyBehavior enemy)
    {
        // TODO: Give player experience for the kill.
    }

    public void SetGamePaused(bool value)
    {
        if (value)
        {
            IsGamePaused = true;
        }
        else
        {
            _gameNotPausedNextFrame = true;
        }
    }

    public void BlockGameplayInput(bool value)
    {
        PlayerInput.OnlyMenus = value;
    }
}