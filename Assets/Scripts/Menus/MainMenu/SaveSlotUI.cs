using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSlotUI : MonoBehaviour {

	public int SlotNumber;
	public Text ChapterName;
	public Color TextColor;

	[SerializeField]
	private Image _img;

	void Start()
	{
		//_img = GetComponent<Image>();

		if (SlotNumber < SaveMenuManager.saves.Count) {
			GameState save = SaveMenuManager.saves [SlotNumber];
			ChapterName.text = save.CurrentScene;
		} else {
			ChapterName.text = "NO SAVEGAME";
		}
	}

	public void LoadSaveFile(){
		if (SlotNumber < SaveMenuManager.saves.Count) {
			GameState save = SaveMenuManager.saves [SlotNumber];
			File.WriteAllText ("SSS.txt", SlotNumber.ToString ());
			SceneManager.LoadScene (save.CurrentScene);
		} 
	}
}