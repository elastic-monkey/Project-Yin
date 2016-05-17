using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement), typeof(AbilitiesManager))]
[RequireComponent(typeof(Experience))]
public class PlayerBehavior : WarriorBehavior
{
    private PlayerMovement _playerMovement;
    private AbilitiesManager _abilities;
    private Experience _experience;

    public Experience Experience
    {
        get
        {
            if (_experience == null)
                _experience = GetComponent<Experience>();

            return _experience;
        }
    }

    public PlayerMovement PlayerMovement
    {
        get
        {
            if (_playerMovement == null)
                _playerMovement = GetComponent<PlayerMovement>();

            return _playerMovement;
        }
    }

    public AbilitiesManager Abilities
    {
        get
        {
            if (_abilities == null)
                _abilities = GetComponent<AbilitiesManager>();

            return _abilities;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if (!Health.Alive)
            return;

        if (Health.CurrentHealth <= 0)
        {
            _gameManager.OnPlayerDeath(this);

            PlayerDeath();

            return;
        }

        if (!PlayerInput.GameplayBlocked)
        {
            Abilities.ApplyAbilities();

            Attack.ApplyAttack();

            if (!Attack.Attacking)
                Defense.ApplyDefense();

            PlayerMovement.CanMove = !Attack.Attacking && !Defense.Defending;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Tags.DangerArea.TagToString()))
        {
            var dangerArea = other.GetComponent<EnemyArea>();
            dangerArea.AddPlayer(this);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.CompareTag(Tags.DangerArea.TagToString()))
        {
            var dangerArea = other.GetComponent<EnemyArea>();
            dangerArea.RemovePlayer();
        } 
    }

    void FixedUpdate()
    {
        if (!PlayerInput.GameplayBlocked)
        {
            PlayerMovement.ApplyMovement();
        }
    }
}
