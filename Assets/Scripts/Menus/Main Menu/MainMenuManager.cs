using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	private static MainMenuManager _instance;

	public MenuSoundManager SoundManager;

	public static MainMenuManager Instance
	{
		get
		{
			if (_instance == null)
			{
				var objs = FindObjectsOfType<MainMenuManager>();
				if (objs.Length == 0)
					Debug.LogError("There is no instantiated MainMenuManager");
				else
					_instance = objs[0];
			}

			return _instance;
		}
	}

	private void Awake()
	{
		Random.seed = System.DateTime.Now.Millisecond;

		_instance = this;

		SoundManager.InGame = false;
	}
}
