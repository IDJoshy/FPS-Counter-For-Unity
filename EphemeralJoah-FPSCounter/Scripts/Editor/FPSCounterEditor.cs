using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(FPSCounter))]
public class FPSCounterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FPSCounter counter = (FPSCounter)target;

        counter.statsTransform = EditorGUILayout.ObjectField("Stats Transform", counter.statsTransform, typeof(Transform), true) as Transform;
        
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Enable", EditorStyles.boldLabel);

        if(GUILayout.Button("Activate"))
        {
            counter.ActivateCounter(true);
        }
        if(GUILayout.Button("De-Activate"))
        {
            counter.ActivateCounter(false);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Base", EditorStyles.boldLabel);
        counter.framerateText = EditorGUILayout.ObjectField("Framerate", counter.framerateText, typeof(TMP_Text), true) as TMP_Text;
        counter.avgFramerateText = EditorGUILayout.ObjectField("Average Framerate", counter.avgFramerateText, typeof(TMP_Text), true) as TMP_Text;
        counter.minFrameRateText = EditorGUILayout.ObjectField("Min Framerate", counter.minFrameRateText, typeof(TMP_Text), true) as TMP_Text;
        counter.maxFramerateText = EditorGUILayout.ObjectField("Max Framerate", counter.maxFramerateText, typeof(TMP_Text), true) as TMP_Text;

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        counter.useStaticColors = EditorGUILayout.Toggle(counter.useStaticColors, GUILayout.MaxWidth(15));
        EditorGUILayout.LabelField("Static Colors", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        if (!counter.useStaticColors)
        {
            counter.highFPSColor = EditorGUILayout.ColorField("High", counter.highFPSColor);
            counter.mediumFPSColor = EditorGUILayout.ColorField("Medium", counter.mediumFPSColor);
            counter.lowFPSColor = EditorGUILayout.ColorField("Low", counter.lowFPSColor);
        }
        else
        {
            counter.staticInfoColor = EditorGUILayout.ColorField("Main Color", counter.staticInfoColor);
        }        
    }
}
