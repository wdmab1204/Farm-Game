using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : Singleton<MapData>
{
    public MapDataScriptableObject[] mdso;
    public int index
    {
        get
        {
            return mdso.Length;
        }
    }
    public MapDataScriptableObject GetMapData(int index)
    {
        return mdso[index];
    }
}
