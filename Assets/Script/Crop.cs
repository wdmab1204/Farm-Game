using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : Item
{
    public CropType croptype;
    public Crop(int id, string name, string itemDescription, ItemType type, CropType croptype) : base(id,name, itemDescription, type)
    {
        this.croptype = croptype;
    }
    public enum CropType
    {
        Cereal, //곡류
        Bulbous, //구근류
        Fruit, //과실류
        Vegetable //채소류
    }
}
