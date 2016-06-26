using UnityEngine;
using Utilities;

public abstract class PlayerInteraction : MonoBehaviour
{
    [Range(1, 8)]
    public float InteractionRadius = 2f;
    public string PromptText;
    public Axes ActivateKey;
    public bool IsInsideRadius, Ongoing;

    public GameManager GameManager
    {
        get
        {
            return GameManager.Instance;
        }
    }

    private bool _startNextFrame = false, _stopNextFrame = false;

    protected virtual void Awake()
    {
        Ongoing = false;
        IsInsideRadius = false;
    }

    protected virtual void Update()
    {
        if (GameManager.IsGamePaused)
            return;

        var inside = Vector3Helper.SqrDistanceXZ(transform.position, GameManager.Player.transform.position) <= (InteractionRadius * InteractionRadius);

        if (inside != IsInsideRadius)
        {
            IsInsideRadius = inside;
            if (inside)
            {
                OnRadiusEnter();
            }
            else
            {
                OnRadiusExit();
            }
        }

        if (!IsInsideRadius)
            return;

        if (!Ongoing && PlayerInput.IsButtonDown(ActivateKey))
        {
            StartInteraction();
        }
    }

    protected virtual void LateUpdate()
    {
        // The methods below are to avoid pressing and key and
        //  triggering new response in the same frame.
        if (_startNextFrame)
        {
            _startNextFrame = false;
            Ongoing = true;
        }

        if (_stopNextFrame)
        {
            _stopNextFrame = false;
            Ongoing = false;
            PlayerInput.OnlyMenus = false;
        }
    }

    public virtual void StartInteraction()
    {
        GameManager.InteractionPrompt.SetVisible(false);
        _startNextFrame = true;
        PlayerInput.OnlyMenus = true;
    }

    public virtual void StopInteraction()
    {
        GameManager.InteractionPrompt.SetVisible(IsInsideRadius, ActivateKey, PromptText);
        _stopNextFrame = true;
    }

    protected virtual void OnRadiusEnter()
    {
        GameManager.InteractionPrompt.SetVisible(true, ActivateKey, PromptText);
    }

    protected virtual void OnRadiusExit()
    {
        GameManager.InteractionPrompt.SetVisible(false);
    }

    #region Editor Only

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawCircleArena(transform.position + Vector3.up, InteractionRadius, 16, Color.cyan);
    }

    #endregion
}
