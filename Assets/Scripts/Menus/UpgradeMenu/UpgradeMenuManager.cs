using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenuManager : MenuManager
{

    public enum Actions
    {
        UpgradeHealth,
        UpgradeStamina,
        UpgradeSpeed,
        UpgradeShield,
        UpgradeStrenght,
        CloseMenu
    }

    public NavMenu UpgradeMenu;
    public Text AvailableSP;

    private GameManager _gameManager;
    private PlayerBehavior _player;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _player = _gameManager.Player;
    }

    private void Update()
    {
        //TODO Change to proper keys
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_gameManager.GamePaused)
            {
                if (UpgradeMenu.IsActive)
                {
                    OnUpgradeMenu(false);
                }
            }
            else
            {
                OnUpgradeMenu(true);
            }
        }
    }

    private void OnUpgradeMenu(bool value)
    {
        _gameManager.SetGamePaused(value);

        UpdateAvailableSP();
        UpgradeMenu.SetActive(value);
    }

    public override void OnAction(object action, object data)
    {
        var actionEnum = (Actions)action;
		var level = (int)data;

        switch (actionEnum)
        {
            case Actions.UpgradeHealth:
                Debug.Log("Upgrading Health To Level " + level);
                UpgradeHealth(level);
                break;

            case Actions.UpgradeStamina:
                Debug.Log("Upgrading Stamina");
                UpgradeStamina(level);
                break;

            case Actions.UpgradeSpeed:
                Debug.Log("Upgrading Speed");
                UpgradeAbility(Ability.AbilityType.Speed, level);
                break;

            case Actions.UpgradeShield:
                Debug.Log("Upgrading Shield");
                UpgradeAbility(Ability.AbilityType.Shield, level);
                break;

            case Actions.UpgradeStrenght:
                Debug.Log("Upgrading Strenght");
                UpgradeAbility(Ability.AbilityType.Strength, level);
                break;

            case Actions.CloseMenu:
                OnUpgradeMenu(false);
                break;
        }
    }

	private void UpgradeHealth(int level)
    {
        _player.Health.Upgrade(level);
        UpdateAvailableSP();
    }

	private void UpgradeStamina(int level)
    {
        _player.Stamina.Upgrade(level);
        UpdateAvailableSP();
    }

	private void UpgradeAbility(Ability.AbilityType type, int level)
    {
        _player.Abilities.UpgradeAbility(type, level);
        UpdateAvailableSP();
    }

    private void UpdateAvailableSP()
    {
		AvailableSP.text = _player.Experience.SkillPoints.ToString();
    }
}
