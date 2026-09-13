using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(objectControler))]
public class objectControlerEditor : Editor
{
    SerializedProperty ThisObjectType;
    SerializedProperty ElementalCombinerWidget;
    SerializedProperty MrsIncubatorWidget;
    SerializedProperty HotelWidget;

    private void OnEnable()
    {
        ThisObjectType = serializedObject.FindProperty("ThisObjectType");
        ElementalCombinerWidget = serializedObject.FindProperty("ElementalCombinerWidget");
        MrsIncubatorWidget = serializedObject.FindProperty("MrsIncubatorWidget");
        HotelWidget = serializedObject.FindProperty("HotelWidget");
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
            case (int)objectControler.ObjectTypes.Opum:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Hotel", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(HotelWidget);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
