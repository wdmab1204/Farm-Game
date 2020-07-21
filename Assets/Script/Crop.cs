using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop
{
    public Item item { get; }
    public CropType croptype { get; }
    public Crop(Item item, CropType cropType)
    {
        this.item = item;
        this.croptype = cropType;
    }
    public enum CropType
    {
        Cereal, //곡류
        Bulbous, //구근류
        Fruit, //과실류
        Vegetable //채소류
    }
}
