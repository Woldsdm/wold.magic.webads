using MagicWebAds;
using UnityEditor;

[CustomEditor(typeof(UIAdView))]
[CanEditMultipleObjects]
public class UIAdViewEditor : Editor
{
    SerializedProperty color;
    SerializedProperty launchOnEnable;
    SerializedProperty loadOnEnable;
    SerializedProperty showOnLoad;
    SerializedProperty hideWhenDisabled;
    SerializedProperty listener;

    void OnEnable()
    {
        color = serializedObject.FindProperty("m_Color");
        launchOnEnable = serializedObject.FindProperty("launchOnEnable");
        loadOnEnable = serializedObject.FindProperty("loadOnEnable");
        showOnLoad = serializedObject.FindProperty("showOnLoad");
        hideWhenDisabled = serializedObject.FindProperty("hideWhenDisabled");
        listener = serializedObject.FindProperty("listener");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(color);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(launchOnEnable);
        EditorGUILayout.PropertyField(loadOnEnable);
        EditorGUILayout.PropertyField(showOnLoad);
        EditorGUILayout.PropertyField(hideWhenDisabled);
        EditorGUILayout.PropertyField(listener);

        serializedObject.ApplyModifiedProperties();
    }
}
