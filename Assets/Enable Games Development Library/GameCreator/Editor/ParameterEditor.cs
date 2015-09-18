using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public enum ParameterType {
    Bool,
    StringList,
    Range
}

[CustomEditor(typeof(GameCreatorPanel))]
public class ParameterEditor : Editor {
    private ReorderableList boolParamList;
    private ReorderableList rangeParamList;
    private ReorderableList stringListParamList;

    private void OnEnable() {
        boolParamList = new ReorderableList(serializedObject, serializedObject.FindProperty("boolParameters"), true, true, true, true);
        boolParamList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Bool Parameters (Category/Name/Value)");
        };
        boolParamList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = boolParamList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                float totalWidth = rect.width;
                float nameWidth = totalWidth * .45f;
                float toggleWidth = totalWidth * .1f;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, nameWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("categoryName"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + nameWidth, rect.y, nameWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("parameterName"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + nameWidth * 2, rect.y, toggleWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("defaultValue"), GUIContent.none);
            };

        rangeParamList = new ReorderableList(serializedObject, serializedObject.FindProperty("rangeParameters"), true, true, true, true);
        rangeParamList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Range Parameters (Category/Name/Min/Max/Default/Tick)");
        };
        rangeParamList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = rangeParamList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                float countedWidth = 0;
                float totalWidth = rect.width;
                float nameWidth = totalWidth * .3f;
                float floatWidth = totalWidth * .4f / 4f;
                EditorGUI.PropertyField(
                    new Rect(rect.x + countedWidth, rect.y, nameWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("categoryName"), GUIContent.none);
                countedWidth += nameWidth;
                EditorGUI.PropertyField(
                    new Rect(rect.x + countedWidth, rect.y, nameWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("parameterName"), GUIContent.none);
                countedWidth += nameWidth;
                EditorGUI.PropertyField(
                    new Rect(rect.x + countedWidth, rect.y, floatWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("minValue"), GUIContent.none);
                countedWidth += floatWidth;
                EditorGUI.PropertyField(
                    new Rect(rect.x + countedWidth, rect.y, floatWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("maxValue"), GUIContent.none);
                countedWidth += floatWidth;

                EditorGUI.PropertyField(
                    new Rect(rect.x + countedWidth, rect.y, floatWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("defaultValue"), GUIContent.none);
                countedWidth += floatWidth;

                EditorGUI.PropertyField(
                    new Rect(rect.x + countedWidth, rect.y, floatWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("tick"), GUIContent.none);
                countedWidth += floatWidth;
            };

        stringListParamList = new ReorderableList(serializedObject, serializedObject.FindProperty("stringListParameters"), true, true, true, true);
        stringListParamList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "List Parameters (Category/Name/Strings...)");
        };
        stringListParamList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = stringListParamList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;// element.CountInProperty() * EditorGUIUtility.singleLineHeight;
                float totalWidth = rect.width;
                float nameWidth = totalWidth * .2f;
                float listWidth = totalWidth * .5f;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, nameWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("categoryName"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + nameWidth, rect.y, nameWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("parameterName"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + nameWidth * 2, rect.y, listWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("strings"), true);
            };
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        DrawDefaultInspector();
        /*boolParamList.DoLayoutList();
        rangeParamList.DoLayoutList();
        stringListParamList.DoLayoutList();*/
        serializedObject.ApplyModifiedProperties();
    }
}
