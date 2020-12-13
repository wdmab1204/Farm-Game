using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    equip, //장비
    use, //소비
    etc, //기타
    crop //작물
}

public enum CropType
{
    Nothing, //작물이 아님
    Cereal, //곡류
    Bulbous, //구근류
    Fruit, //과실류
    Vegetable //채소류
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string itemDescription;
    public int count;
    public int sellingPrice;
    public int purchasePrice;
    public float growTime;
    public int minDropCount;
    public int maxDropCount;

    private Sprite icon;
    public Sprite Icon
    {
        get
        {
            if (icon == null)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("item");
                foreach (Sprite sprite in sprites)
                {
                    if (sprite.name.Equals(id.ToString()))
                    {
                        this.icon = sprite;
                        break;
                    }
                }

                if (icon == null)
                {
                    return null;
                }
            }
            return icon;
        }
    }
    public ItemType type;
    public CropType cropType;

    public Item(int id, string name, string itemDescription, ItemType type, CropType cropType, int sellingPrice, int purchasePrice, float growTime, int minDropCount, int maxDropCount, int count = 1)
    {
        this.id = id;
        this.name = name;
        this.itemDescription = itemDescription;
        this.type = type;
        this.cropType = cropType;
        this.count = count;
        this.sellingPrice = sellingPrice;
        this.purchasePrice = purchasePrice;
        this.growTime = growTime;
        this.minDropCount = minDropCount;
        this.maxDropCount = maxDropCount;
    }

    public Item(ItemScriptableObject obj)
    {
        this.id = obj.id;
        this.name = obj.name;
        this.itemDescription = obj.description;
        this.type = obj.type;
        this.cropType = obj.cropType;
        this.count = obj.count;
        this.sellingPrice = obj.sellingPrice;
        this.purchasePrice = obj.purchasePrice;
        this.growTime = obj.growTime;
        this.minDropCount = obj.minDropCount;
        this.maxDropCount = obj.maxDropCount;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public override bool Equals(object obj)
    {
        Item item = (Item)obj;
        if (id == item.id &&
            name.Equals(item.name) &&
            itemDescription.Equals(item.itemDescription) &&
            type == item.type)
            return true;

        return false;
    }

    public override string ToString()
    {
        return "\n" +
            "Id : " + id + "\n"
            + "Name : " + name + "\n"
            + "Description : " + itemDescription + "\n"
            + "Count : " + count + "\n"
            + "SellingPrice : " + sellingPrice + "\n"
            + "PurchasePrice : " + purchasePrice;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
