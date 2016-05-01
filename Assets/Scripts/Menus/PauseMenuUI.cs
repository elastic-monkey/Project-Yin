
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{
	public RectTransform Container;
	public Button Resume, LoadLastCheckpoint, ReturnToMainMenu;
	public float AnimSpeed;
	public Utils.WindowPositions HidePosition, ShowPosition;

	[SerializeField]
	bool _visible;
	Coroutine _windowAnim;
	RectTransform _rectTransform;

	void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		Resume.interactable = false;
		LoadLastCheckpoint.interactable = false;

		_visible = true;
		SetVisible(false);
	}

	void Update()
	{
		if (PlayerInput.IsButtonDown(Axis.Escape))
		{
			PlayerInput.Blocked = true;
			Time.timeScale = 0f;
			SetVisible (true);
			Resume.interactable = true;
			LoadLastCheckpoint.interactable = true;
		}
	}

	public void SetVisible(bool value)
	{
		if (_visible != value)
		{
			_visible = value;

			if (_windowAnim != null)
				StopCoroutine(_windowAnim);

			_windowAnim = StartCoroutine(AnimateWindow());
		}
	}

	IEnumerator AnimateWindow()
	{
		Vector2 targetMin, targetMax;
		if (_visible)
		{
			Utils.GetAnchorsForWindowPosition(ShowPosition, out targetMin, out targetMax);
			while (true)
			{
				if (Utils.LerpRectTransformAnchors(_rectTransform, targetMin, targetMax, Time.unscaledDeltaTime * AnimSpeed))
					yield break;

				yield return null;
			}
		}
		else
		{
			Utils.GetAnchorsForWindowPosition(HidePosition, out targetMin, out targetMax);
			while (true)
			{
				if (Utils.LerpRectTransformAnchors(_rectTransform, targetMin, targetMax, Time.unscaledDeltaTime * AnimSpeed))
					yield break;

				yield return null;
			}
		}
	}

	public void OnResumePressed()
	{
		SetVisible(false);
		PlayerInput.Blocked = false;
		Time.timeScale = 1f;
	}

	public void OnLoadLastCheckpointPressed()
	{
		SaveManager.LoadCheckpoint = true;
		OnResumePressed ();
	}

	public void OnReturnToMainMenuPressed(){
		SceneManager.LoadScene ("MainMenu");
	}
}
