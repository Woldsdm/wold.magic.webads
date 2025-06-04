using MagicWebAds;
using UnityEditor;

[CustomEditor(typeof(AdButtonImage))]
[CanEditMultipleObjects]
public class AdButtonImageEditor : Editor
{
    SerializedProperty spriteProp;
    SerializedProperty activationDelay;
    SerializedProperty onClicked;
    SerializedProperty steps;

    void OnEnable()
    {
        spriteProp = serializedObject.FindProperty("m_Sprite");
        activationDelay = serializedObject.FindProperty("activationDelay");
        onClicked = serializedObject.FindProperty("onClicked");
        steps = serializedObject.FindProperty("steps");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(spriteProp);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(activationDelay);
        EditorGUILayout.PropertyField(onClicked);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(steps);

        serializedObject.ApplyModifiedProperties();
    }
}