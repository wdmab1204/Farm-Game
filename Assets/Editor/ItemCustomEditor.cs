using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        ItemScriptableObject item = EditorUtility.InstanceIDToObject(instanceId) as ItemScriptableObject;
        if(item != null)
        {
            ItemEditorWindow.Open(item);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(ItemScriptableObject))]
public class ItemCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor"))
        {

        }
    }
}
