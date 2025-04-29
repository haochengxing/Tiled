using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(TouchCenterScaleUI), true)]
[CanEditMultipleObjects]
public class TouchCenterScaleUIEditor : ScrollRectEditor
{
    SerializedProperty m_minScale;
    SerializedProperty m_maxScale;
    SerializedProperty m_scaleSensitivity;
    SerializedProperty m_distanceScale;
    SerializedProperty m_rectTransform;
    protected override void OnEnable()
    {
        base.OnEnable();
        m_minScale = serializedObject.FindProperty("minScale");
        m_maxScale = serializedObject.FindProperty("maxScale");
        m_scaleSensitivity = serializedObject.FindProperty("scaleSensitivity");
        m_distanceScale = serializedObject.FindProperty("distanceScale");
        m_rectTransform = serializedObject.FindProperty("rectTransform");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_minScale, new GUIContent("minScale"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(m_maxScale, new GUIContent("maxScale"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(m_scaleSensitivity, new GUIContent("scaleSensitivity"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(m_distanceScale, new GUIContent("distanceScale"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(m_rectTransform, new GUIContent("rectTransform"), GUILayout.Height(20));
        serializedObject.ApplyModifiedProperties();
    }
}