using MagicWebAds;
using UnityEditor;

[CustomEditor(typeof(AdButtonImage))]
[CanEditMultipleObjects]
public class AdButtonImageEditor : Editor
{
    SerializedProperty spriteProp;
    SerializedProperty onClicked;

    void OnEnable()
    {
        spriteProp = serializedObject.FindProperty("m_Sprite");
        onClicked = serializedObject.FindProperty("onClicked");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(spriteProp);
        EditorGUILayout.PropertyField(onClicked);

        serializedObject.ApplyModifiedProperties();
    }
}