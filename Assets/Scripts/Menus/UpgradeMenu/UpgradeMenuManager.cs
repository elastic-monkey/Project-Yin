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

        switch (actionEnum)
        {
            case Actions.UpgradeHealth:
                Debug.Log("Upgrading Health");
                UpgradeHealth();
                break;

            case Actions.UpgradeStamina:
                Debug.Log("Upgrading Stamina");
                UpgradeStamina();
                break;

            case Actions.UpgradeSpeed:
                Debug.Log("Upgrading Speed");
                UpgradeAbility(Ability.Type.Speed);
                break;

            case Actions.UpgradeShield:
                Debug.Log("Upgrading Shield");
                UpgradeAbility(Ability.Type.Shield);
                break;

            case Actions.UpgradeStrenght:
                Debug.Log("Upgrading Strenght");
                UpgradeAbility(Ability.Type.Strength);
                break;

            case Actions.CloseMenu:
                OnUpgradeMenu(false);
                break;
        }
    }

    private void UpgradeHealth()
    {
        _player.Health.Upgrade();
        UpdateAvailableSP();
    }

    private void UpgradeStamina()
    {
        _player.Stamina.Upgrade();
        UpdateAvailableSP();
    }

    private void UpgradeAbility(Ability.Type type)
    {
        _player.Abilities.UpgradeAbility(type);
        UpdateAvailableSP();
    }

    private void UpdateAvailableSP()
    {
        AvailableSP.text = "Skill Points: " + _player.Experience.SkillPoints;
    }
}
