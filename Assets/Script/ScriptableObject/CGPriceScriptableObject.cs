using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CGPrice", order = 1)]
public class CGPriceScriptableObject : ScriptableObject
{
    public int[] price;
}
