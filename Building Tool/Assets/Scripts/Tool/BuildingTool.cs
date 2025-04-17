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

    enum COMPONENTTYPE
    {
        None,
        Floor,
        Wall,
        Roof
    }

    private List<GameObject> components = new List<GameObject>();

    GameObject pointer;
    private void GenerateComponent(int listPos, Vector3 pos)
    {
        Instantiate(settings.compList[listPos], pos, Quaternion.identity);
    }

    Ray ray;
    void OnSceneGUI(SceneView sceneView)
    {
        Event currEvent = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(currEvent.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

        }
    }

    Settings_SO settings;
    void HandleSettings()
    {
        settings = (Settings_SO)EditorGUILayout.ObjectField("Settings_SO", settings, typeof(Settings_SO), false);
    }

    bool isToolOn = false;
    COMPONENTTYPE typeSelected = COMPONENTTYPE.None;
    private void OnGUI()
    {
        HandleSettings();
        
        if(isToolOn)
        {
            SceneView.duringSceneGui += OnSceneGUI;

            GUILayout.BeginVertical();

            typeSelected = (COMPONENTTYPE)EditorGUILayout.EnumPopup("Component Selected", typeSelected);

            GUILayout.EndVertical();
        }

        else
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            isToolOn = false;
            DestroyImmediate(pointer);
        }

        if(GUILayout.Button("Tool On"))
        {    
            isToolOn = !isToolOn;
        }
    }

    private void OnDisable()
    {    
        SceneView.duringSceneGui -= OnSceneGUI;
        if(pointer != null)
        DestroyImmediate(pointer);
    }
}