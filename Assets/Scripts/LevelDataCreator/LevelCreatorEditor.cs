
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;



[CustomEditor(typeof(LevelDataCreator))]
public class IncomeDataCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelDataCreator levelCreator = (LevelDataCreator)target;

        GUILayout.Space(25f);

        EditorGUI.BeginChangeCheck();

        GUILayout.Label("LevelData");

        levelCreator.TempLevelData = EditorGUILayout.ObjectField("", levelCreator.TempLevelData, typeof(LevelData), true) as LevelData;

        GUILayout.Label("Number Of Level To Be Created");
        levelCreator.LevelNumber = EditorGUILayout.IntField("Number Of Level", levelCreator.LevelNumber);

        GUILayout.Space(25f);

        if (GUILayout.Button("CreateLevel"))
        {
            levelCreator.CreateLevel();
        }
    }

}
#endif

