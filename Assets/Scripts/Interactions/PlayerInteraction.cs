using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class PlayerInteraction : MonoBehaviour
{
    [Range(1, 4)]
    public float InteractionRadius = 2f;
    public string PromptText;
    public Axis ActivateKey;
    public bool CanBeActivated, Active;

    protected GameManager _gameManager;
    protected InteractionPrompt _interactionPrompt;

    private SphereCollider _collider;
    private bool _activeNextFrame;

    public SphereCollider Collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<SphereCollider>();

            return _collider;
        }
    }

    protected virtual void Awake()
    {
        Collider.isTrigger = true;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _interactionPrompt = _gameManager.InteractionPrompt;
    }

    private void LateUpdate()
    {
        if (_activeNextFrame)
        {
            _activeNextFrame = false;
            Active = true;
        }
    }

    public virtual void SetActive(bool value)
    {
        BlockInput(value);

        _activeNextFrame = value;
        Active = false;
    }

    private void OnValidate()
    {
        Collider.radius = InteractionRadius;
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider == _gameManager.Player.MainCollider)
        {
            CanBeActivated = true;
            _interactionPrompt.Show(true, this);
        }
    }

   protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider == _gameManager.Player.MainCollider)
        {
            CanBeActivated = false;
            _interactionPrompt.Show(false, this);
        }
    }

    protected void BlockInput(bool block)
    {
        PlayerInput.GameplayBlocked = block;
    }
}
