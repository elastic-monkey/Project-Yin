using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Currency : MonoBehaviour {

	public float MaxCredits = 999999f;
	public Text PlayerCredits;

	private float _currentCredits;

	public float CurrentCredits{
		get{
			return _currentCredits;
		}
		set{
			_currentCredits = Mathf.Clamp(value + _currentCredits, 0f, MaxCredits);
			PlayerCredits.text = _currentCredits.ToString ();
		}
	}

	public void AddCredits(float credits){
		CurrentCredits += credits;
	}

	public void RemoveCredits(float credits){
		AddCredits (-credits);
	}

	void Awake(){
		// TODO LOAD CREDITS FROM SAVEFILE
		CurrentCredits = 1000f;
	}
}
