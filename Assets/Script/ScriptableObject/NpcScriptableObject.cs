using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Npc", menuName = "ScriptableObjects/Npc", order = 1)]

public class NpcScriptableObject : ScriptableObject
{
    public ItemScriptableObject[] items;
}
