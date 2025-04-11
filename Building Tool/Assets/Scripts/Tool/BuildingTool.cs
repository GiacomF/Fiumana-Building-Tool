using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingTool : EditorWindow
{
    [MenuItem("Tools/BuildingTool")]
    public static void ShowWindow()
    {
        GetWindow<BuildingTool>("Build Tool");
    }

    void OnEnable()
    {
        
    }

    public List<GameObject> compList = new List<GameObject>();
    private void GenerateComponent(int listPos)
    {
        Instantiate(compList[listPos], new Vector3(0,0,0), Quaternion.identity);
    }

    void HandlePrefabList()
    {

        SerializedObject serObj = new SerializedObject(this);
        SerializedProperty listProp = serObj.FindProperty("compList");
        
        serObj.Update();

        GUILayout.Label("ComponentsList", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(listProp, true);

        serObj.ApplyModifiedProperties();
    }

    private void OnGUI()
    {
        HandlePrefabList();

        GUILayout.BeginVertical();

        if(GUILayout.Button("Floor"))
        {
            GenerateComponent(0);
        }

        if(GUILayout.Button("Wall"))
        {
            GenerateComponent(1);
        }

        if(GUILayout.Button("Roof"))
        {
            GenerateComponent(2);
        }

        GUILayout.EndVertical();

        this.SaveChanges();
    }
}
