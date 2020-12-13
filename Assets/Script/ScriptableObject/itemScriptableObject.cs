using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public ItemType type;
    public CropType cropType;
    public int count;
    public int sellingPrice;
    public int purchasePrice;
    public float growTime;
    public int minDropCount;
    public int maxDropCount;
}
