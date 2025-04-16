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
            pointer.transform.position = hit.point;
        }
    }

    Settings_SO settings;
    void HandleSettings()
    {
        settings = (Settings_SO)EditorGUILayout.ObjectField("Settings_SO", settings, typeof(Settings_SO), false);
    }

    bool isToolOn = false;
    private void OnGUI()
    {
        HandleSettings();
        
        if(pointer != null)
        {
            pointer.GetComponent<Pointer>().AdjustPosition(ray);
        }

        if(GUILayout.Button("Tool On"))
        {    
            if(!isToolOn)
            {
                isToolOn = true;

                pointer = Instantiate(settings.pointer);

                SceneView.duringSceneGui += OnSceneGUI;

                GUILayout.BeginVertical();

                if(pointer != null)
                {
                    if(GUILayout.Button("Floor"))
                    {
                        GenerateComponent(0, pointer.transform.position);
                    }

                    if(GUILayout.Button("Wall"))
                    {
                        GenerateComponent(1, pointer.transform.position);
                    }

                    if(GUILayout.Button("Roof"))
                    {
                        GenerateComponent(2, pointer.transform.position);
                    }
                }

                GUILayout.EndVertical();

                this.SaveChanges();
            }

            else
            {
                SceneView.duringSceneGui -= OnSceneGUI;
                isToolOn = false;
                DestroyImmediate(pointer);
            }
        }
    }

    private void OnDisable()
    {    
        SceneView.duringSceneGui -= OnSceneGUI;
        if(pointer != null)
        DestroyImmediate(pointer);
    }
}