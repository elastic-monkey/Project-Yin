using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interaction : MonoBehaviour {

	public RectTransform InteractionPrompt;
	public Tags PlayerTag;

	protected int _playerHash;
	protected Text _interactionText;
	protected Text _dialogueText;
	protected Text _title;

	protected PlayerBehavior _player;

	protected virtual void Awake(){
		_playerHash = PlayerTag.ToHash ();
		InteractionPrompt.gameObject.SetActive (false);
		_interactionText = InteractionPrompt.GetComponentInChildren<Text>();
	}

	void OnTriggerEnter(Collider collider){
		var colliderHash = collider.tag.GetHashCode ();
		if (colliderHash == _playerHash) {
			InteractionPrompt.gameObject.SetActive (true);
			_player = collider.GetComponentInParent<PlayerBehavior>();
		}
	}
		
	void OnTriggerExit(Collider collider){
		InteractionPrompt.gameObject.SetActive (false);
		if (_player != null) {
			if (collider == _player.GetComponentInChildren<Collider> ()) {
				_player = null;
			}
		}
	}

	protected void BlockInput(bool block){
		if (_player) {
			_player.Input.Blocked = block;
		}
	}
}
