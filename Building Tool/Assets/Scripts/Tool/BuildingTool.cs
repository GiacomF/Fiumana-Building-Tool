using UnityEditor;
using UnityEngine;

public class BuildingTool : EditorWindow
{
    [MenuItem("Tools/BuildingTool")]
    public static void ShowWindow()
    {
        GetWindow<BuildingTool>("Build Tool");
    }

    GameObject pointer;

    private void GenerateComponent(int listPos, Vector3 pos)
    {
        Instantiate(settings.compList[listPos], pos, Quaternion.identity);
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
        
        if(GUILayout.Button("Tool On"))
        {    
            if(!isToolOn)
            {
                isToolOn = true;

                pointer = Instantiate(settings.pointer);

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
                isToolOn = false;
                DestroyImmediate(pointer);
            }
        }
    }

    private void OnDisable()
    {
        if(pointer != null)
        DestroyImmediate(pointer);
    }
}
