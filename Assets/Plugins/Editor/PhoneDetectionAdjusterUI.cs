using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PhoneDetectionAdjuster))]
public class PhoneDetectionAdjusterUI : Editor {
    public override void OnInspectorGUI()
    {
        PhoneDetectionAdjuster pda = (PhoneDetectionAdjuster)target;

        DrawDefaultInspector();

        GUILayout.Label("Scale: " + pda._3by2Scale + "\nPosition: " + pda._3by2Position);
        if (GUILayout.Button("Set 3 by 2 Values"))
        {
            pda._3by2Position = Selection.activeTransform.localPosition;
            pda._3by2Scale = Selection.activeGameObject.transform.localScale;
        }
        if (GUILayout.Button("Move to 3 by 2 Values"))
        {
            Selection.activeTransform.localPosition = pda._3by2Position;
            Selection.activeGameObject.transform.localScale = pda._3by2Scale;
        }


        GUILayout.Label("Scale: " + pda._16by9Scale + "\nPosition: " + pda._16by9Position);
        if (GUILayout.Button("Set 16 by 9 Values"))
        {
            pda._16by9Position = Selection.activeTransform.localPosition;
            pda._16by9Scale = Selection.activeGameObject.transform.localScale;
        }
        if (GUILayout.Button("Move to 16 by 9 Values"))
        {
            Selection.activeTransform.localPosition = pda._16by9Position;
            Selection.activeGameObject.transform.localScale = pda._16by9Scale;
        }


        GUILayout.Label("Scale: " + pda._4by3Scale + "\nPosition: " + pda._4by3Position);
        if (GUILayout.Button("Set 4 by 3 Values"))
        {
            pda._4by3Position = Selection.activeTransform.localPosition;
            pda._4by3Scale = Selection.activeGameObject.transform.localScale;
        }
        if (GUILayout.Button("Move to 4 by 3 Values"))
        {
            Selection.activeTransform.localPosition = pda._4by3Position;
            Selection.activeGameObject.transform.localScale = pda._4by3Scale;
        }


        GUILayout.Label("Scale: " + pda._16by10Scale + "\nPosition: " + pda._16by10Position);
        if (GUILayout.Button("Set 16 by 10 Values"))
        {
            pda._16by10Position = Selection.activeTransform.localPosition;
            pda._16by10Scale = Selection.activeGameObject.transform.localScale;
        }
        if (GUILayout.Button("Move to 16 by 10 Values"))
        {
            Selection.activeTransform.localPosition = pda._16by10Position;
            Selection.activeGameObject.transform.localScale = pda._16by10Scale;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(pda);
        }
    }
}
