using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{

    public RectTransform InteractionPrompt;
    public Tags PlayerTag;

    protected Text _interactionText;
    protected Text _dialogueText;
    protected Text _title;

    protected PlayerBehavior _player;

    protected virtual void Awake()
    {
        InteractionPrompt.gameObject.SetActive(false);
        _interactionText = InteractionPrompt.GetComponentInChildren<Text>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerTag.ToString()))
        {
            InteractionPrompt.gameObject.SetActive(true);
            _player = collider.GetComponentInParent<PlayerBehavior>();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        InteractionPrompt.gameObject.SetActive(false);
        if (_player != null)
        {
            if (collider == _player.GetComponentInChildren<Collider>())
            {
                _player = null;
            }
        }
    }

    protected void BlockInput(bool block)
    {
        PlayerInput.GameplayBlocked = block;
    }
}
