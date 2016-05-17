using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class PlayerInteraction : MonoBehaviour
{
    [Range(1, 4)]
    public float InteractionRadius;
    public InteractionsManager.Actions Action;
    public string PromptText;
    public Axis ActivateKey;
    public bool CanBeActivated;

    private InteractionsManager _interactionsManager;
    private SphereCollider _collider;
    private PlayerBehavior _player;
    private bool _canBeOpen;

    public SphereCollider Collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<SphereCollider>();

            return _collider;
        }
    }

    private void Awake()
    {
        Collider.isTrigger = true;
    }

    private void Start()
    {
        _interactionsManager = GameManager.Instance.Interactions;
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (CanBeActivated && PlayerInput.IsButtonDown(ActivateKey))
        {
            CanBeActivated = false;
            _interactionsManager.StartInteraction(Action, this);
        }
    }

    private void OnValidate()
    {
        Collider.radius = InteractionRadius;
    }

    virtual protected void OnTriggerEnter(Collider collider)
    {
        if (collider == _player.MainCollider)
        {
            _interactionsManager.ShowPrompt(true, this);
            CanBeActivated = true;
        }
    }

    virtual protected void OnTriggerExit(Collider collider)
    {
        if (collider == _player.MainCollider)
        {
            _interactionsManager.ShowPrompt(false, this);
            CanBeActivated = false;
        }
    }

    protected void BlockInput(bool block)
    {
        PlayerInput.GameplayBlocked = block;
    }
}
