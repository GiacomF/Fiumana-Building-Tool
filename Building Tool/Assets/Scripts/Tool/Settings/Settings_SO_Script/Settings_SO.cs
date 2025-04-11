using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "SO/ToolSettings")]
public class Settings_SO : ScriptableObject
{
    [SerializeField]
    public List<GameObject> compList;
    [SerializeField]
    public GameObject pointer;
}
