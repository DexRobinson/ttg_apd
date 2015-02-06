using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PhoneDetectionGUI))]
public class PhoneGUIAdjuster : Editor {
    public override void OnInspectorGUI()
    {
        PhoneDetectionGUI pga = (PhoneDetectionGUI)target;

        DrawDefaultInspector();

        GUILayout.Label("Pixel: " + pga._3by2Scale + "\nPosition: " + pga._3by2Position);
        if (GUILayout.Button("Set 3 by 2 Values"))
        {
            pga._3by2Position = Selection.activeTransform.position;
            pga._3by2Scale = Selection.activeGameObject.guiTexture.pixelInset; 
        }
        GUILayout.Label("Pixel: " + pga._16by9Scale + "\nPosition: " + pga._16by9Position);
        if (GUILayout.Button("Set 16 by 9 Values"))
        {
            pga._16by9Position = Selection.activeTransform.position;
            pga._16by9Scale = Selection.activeGameObject.guiTexture.pixelInset; 
        }
        GUILayout.Label("Pixel: " + pga._4by3Scale + "\nPosition: " + pga._4by3Position);
        if (GUILayout.Button("Set 4 by 3 Values"))
        {
            pga._4by3Position = Selection.activeGameObject.transform.position;
            pga._4by3Scale = Selection.activeGameObject.guiTexture.pixelInset; 
        }
        GUILayout.Label("Pixel: " + pga._16by10Scale + "\nPosition: " + pga._16by10Position);
        if (GUILayout.Button("Set 16 by 10 Values"))
        {
            pga._16by10Position = Selection.activeGameObject.transform.position;
            pga._16by10Scale = Selection.activeGameObject.guiTexture.pixelInset;
        }
    }
}
