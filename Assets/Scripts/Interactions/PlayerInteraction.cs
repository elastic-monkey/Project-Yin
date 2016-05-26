using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class PlayerInteraction : MonoBehaviour
{
    [Range(1, 4)]
    public float InteractionRadius = 2f;
    public string PromptText;
    public Axis ActivateKey;
    public bool CanBeTriggered;
    public bool Ongoing;

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

    private void Update()
    {
        if (!Ongoing)
            return;

        if (ShouldStop())
            StopInteraction();
    }

    public virtual void StartInteraction()
    {
        _interactionPrompt.Show(false, this);
        Ongoing = true;
    }

    public virtual void StopInteraction()
    {
        _interactionPrompt.Show(CanBeTriggered, this);
        Ongoing = false;
    }

    public abstract bool ShouldStop();

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider == _gameManager.Player.MainCollider)
        {
            CanBeTriggered = true;
            _interactionPrompt.Show(true, this);
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider == _gameManager.Player.MainCollider)
        {
            CanBeTriggered = false;
            _interactionPrompt.Show(false, this);
        }
    }

    #region Editor Only

    private void OnValidate()
    {
        Collider.radius = InteractionRadius;
    }

    #endregion
}
