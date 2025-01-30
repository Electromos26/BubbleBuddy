using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ButlonEffects))]
public class ButtonEffectsInspector : ButtonEditor
{
    private SerializedProperty duration;
    private SerializedProperty ease;
    private SerializedProperty start;
    private SerializedProperty target;

    protected override void OnEnable()
    {
        base.OnEnable();

        duration = serializedObject.FindProperty("duration");
        ease = serializedObject.FindProperty("ease");
        start = serializedObject.FindProperty("start");
        target = serializedObject.FindProperty("target");

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        serializedObject.Update();
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(duration);
        EditorGUILayout.PropertyField(ease);
        EditorGUILayout.PropertyField(start);
        EditorGUILayout.PropertyField(target);
        
        
        serializedObject.ApplyModifiedProperties();
    }
}
