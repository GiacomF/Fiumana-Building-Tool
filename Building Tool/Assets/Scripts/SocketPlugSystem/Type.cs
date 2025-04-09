using System;
using UnityEngine;

public class Type : MonoBehaviour
{
    [Serializable]
    public enum O_Type
    {
        Wall,
        Floor,
        Roof
    }

    public O_Type type;
}
