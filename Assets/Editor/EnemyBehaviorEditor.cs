using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBehavior))]
public class EnemyBehaviorEditor : Editor
{
    private EnemyBehavior _script;

    public void OnEnable()
    {
        _script = (EnemyBehavior)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        var width = EditorGUIUtility.currentViewWidth;
        var labelWidth = 0.2f * width;
        var fieldWidth = 0.4f * width;
        GUILayout.Label("Attack", GUILayout.Width(labelWidth));
        _script.AttackDefense = ClampStep((int)GUILayout.HorizontalSlider(
            _script.AttackDefense, EnemyBehavior.AttackDefenseSliderMin, EnemyBehavior.AttackDefenseSliderMax, GUILayout.Width(fieldWidth)),
            EnemyBehavior.AttackDefenseSliderMin, EnemyBehavior.AttackDefenseSliderMax, EnemyBehavior.AttackDefenseSliderStep);
        GUILayout.Label("Defense", GUILayout.Width(labelWidth));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Fight", GUILayout.Width(labelWidth));
        _script.Courage = ClampStep((int)GUILayout.HorizontalSlider(
            _script.Courage, EnemyBehavior.CourageSliderMin, EnemyBehavior.AttackDefenseSliderMax, GUILayout.Width(fieldWidth)),
            EnemyBehavior.CourageSliderMin, EnemyBehavior.CourageSliderMax, EnemyBehavior.CourageSliderStep);
        GUILayout.Label("Run Away", GUILayout.Width(labelWidth));
        EditorGUILayout.EndHorizontal();
    }

    int ClampStep(int value, int min, int max, int step)
    {
        value = Mathf.Clamp(value, min, max);

        for (var x = min; x <= max; x += step)
        {
            if (x + step > value)
            {
                var deltaBelow = value - x;
                var deltaTop = step - deltaBelow;
                if (deltaBelow < deltaTop)
                {
                    return x;
                }
                else
                {
                    return x + step;
                }
            }
            else if (x == value)
            {
                return x;
            }
        }

        return value;
    }
}
