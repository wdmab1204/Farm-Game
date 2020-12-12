using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemEditorWindow : ExtendedEditorWIndow
{
    public static void Open(ItemScriptableObject item)
    {
        ItemEditorWindow window = GetWindow<ItemEditorWindow>();
        window.serializedObject = new SerializedObject(item);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("name");
        DrawProperties(currentProperty, true);
    }
}
