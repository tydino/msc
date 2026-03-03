using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveData))]
public class Editor_SaveData : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveData sd = (SaveData)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save"))
        {
            sd.save();
        }
        if (GUILayout.Button("Load"))
        {
            sd.load();
        }
        GUILayout.EndHorizontal();
    }
}
