using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(objectControler))]
public class objectControlerEditor : Editor
{
    SerializedProperty ThisObjectType;
    SerializedProperty ElementalCombinerWidget;
    SerializedProperty MrsIncubatorWidget;

    private void OnEnable()
    {
        ThisObjectType = serializedObject.FindProperty("ThisObjectType");
        ElementalCombinerWidget = serializedObject.FindProperty("ElementalCombinerWidget");
        MrsIncubatorWidget = serializedObject.FindProperty("MrsIncubatorWidget");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(ThisObjectType);

        switch (ThisObjectType.enumValueIndex)
        {
            case (int)objectControler.ObjectTypes.MrsIncubator:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Mrs Incubator", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(MrsIncubatorWidget);
                break;
            case (int)objectControler.ObjectTypes.ElementalCombiner:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Elemental Combiner", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(ElementalCombinerWidget);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
