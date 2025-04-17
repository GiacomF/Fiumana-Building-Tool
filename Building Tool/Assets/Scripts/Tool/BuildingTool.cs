using System.Collections.Generic;
using System.IO;
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
        Floor,
        Roof,
        Wall
    }


    Ray ray;
    void OnSceneGUI(SceneView sceneView)
    {
        Event currEvent = Event.current;

        Ray ray = HandleUtility.GUIPointToWorldRay(currEvent.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, settings.correctLayer) && currPreview != null)
        {
            currPreview.transform.position = hit.point;
            SceneView.RepaintAll();
        }

        float yDeg = currPreview.transform.rotation.y;
        if(yDeg > 360f)
        {
            currPreview.transform.rotation = Quaternion.Euler(0,0,0);
        }

        if(currEvent.type == EventType.MouseDown && currEvent.button == 0)
        {
            PlaceComponent();
        }
    }

    Settings_SO settings;
    void HandleSettings()
    {
        settings = (Settings_SO)EditorGUILayout.ObjectField("Settings_SO", settings, typeof(Settings_SO), false);
    }

    private List<GameObject> components = new List<GameObject>();
    void LoadPrefabs(string path)
    {
        if(!Directory.Exists(path))
        {
            Debug.LogError("Invalid asset path");
            return;
        }

        string[] files = Directory.GetFiles(path, "*.prefab", SearchOption.TopDirectoryOnly);

        components.Clear();
        foreach(string file in files)
        {
            components.Add(AssetDatabase.LoadAssetAtPath<GameObject>(file));
        }

        Debug.Log($"Added {components.Count} components");
    }

    GameObject currPreview;
    COMPONENTTYPE typeSelected;
    void CreatePreview()
    {
        if(currPreview != null)
        {
            DestroyImmediate(currPreview);
        }

        currPreview = Instantiate(components[(int)typeSelected]);
    }

    void PlaceComponent()
    {
        Instantiate(components[(int)typeSelected], currPreview.transform.position, Quaternion.identity);
    }

    bool isToolOn = false;
    string folderPath;
    private void OnGUI()
    {
        HandleSettings();
        
        if(isToolOn)
        {
            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical();

            GUILayout.Label("Load Asset Path", EditorStyles.boldLabel);
            folderPath = GUILayout.TextField(folderPath);

            if(GUILayout.Button("Load Prefabs"))
            {
                LoadPrefabs(folderPath);
            }

            typeSelected = (COMPONENTTYPE)EditorGUILayout.EnumPopup("Component Selected", typeSelected);

            if(EditorGUI.EndChangeCheck())
            {
                if(components.Count > 0)
                CreatePreview();
            }

            if(GUILayout.Button("Rotate component"))
            {
                currPreview.transform.Rotate(0,90,0, Space.World);
            }

            GUILayout.EndVertical();
        }

        if(GUILayout.Button(isToolOn ? "Tool Off" : "Tool On"))
        {    
            isToolOn = !isToolOn;

            if(isToolOn)
            {  
                SceneView.duringSceneGui += OnSceneGUI;
            }
            else
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }

    private void OnDisable()
    {    
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}