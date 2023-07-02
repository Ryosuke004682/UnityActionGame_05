using System.ComponentModel;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITTOR
using UnityEditor;
#endif

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.PropertyField(position , property , label);
        EditorGUI.EndDisabledGroup();
    }
}
#endif
