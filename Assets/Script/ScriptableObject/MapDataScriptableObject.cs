using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 1)]

public class MapDataScriptableObject : ScriptableObject
{
    public Vector2 ajussiPosition;
    public Vector2 halmoneyPosition;
    public Vector2 truckPosition;
    public Vector2[] CGPosition;
}
