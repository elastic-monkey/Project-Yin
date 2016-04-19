using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Stamina))]
public class StaminaEditor : Editor
{
	private Stamina _script;

	public void OnEnable()
	{
		_script = (Stamina)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GUI.enabled = false;
		EditorGUILayout.FloatField("Stamina", _script.CurrentStamina);
		GUI.enabled = true;
	}
}