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
    SerializedProperty filters;
    SerializedProperty listener;
    SerializedProperty adButtons;

    void OnEnable()
    {
        color = serializedObject.FindProperty("m_Color");
        launchOnEnable = serializedObject.FindProperty("launchOnEnable");
        loadOnEnable = serializedObject.FindProperty("loadOnEnable");
        showOnLoad = serializedObject.FindProperty("showOnLoad");
        hideWhenDisabled = serializedObject.FindProperty("hideWhenDisabled");
        filters = serializedObject.FindProperty("filters");
        listener = serializedObject.FindProperty("listener");
        adButtons = serializedObject.FindProperty("adButtons");
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
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(filters);
        EditorGUILayout.PropertyField(adButtons);
        EditorGUILayout.PropertyField(listener);

        serializedObject.ApplyModifiedProperties();
    }
}
