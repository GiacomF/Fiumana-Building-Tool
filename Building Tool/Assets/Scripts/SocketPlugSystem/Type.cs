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

    public O_Type PlugType;

    public bool CompareType(O_Type itype)
    {
        if (itype == PlugType) return true;
        else return false;
    }
}
