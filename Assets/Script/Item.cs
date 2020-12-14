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
    public int count = 1;
    public int sellingPrice;
    public int purchasePrice;
    public float growTime;
    public int minDropCount;
    public int maxDropCount;
    public Sprite Icon;
    public ItemType type;
    public CropType cropType;
    public int minInventoryCount;
    public int maxInventoryCount;

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

    public Item(TBL_Item tBL_Item)
    {
        Init(tBL_Item);
    }

    public Item(BansheeGz.BGDatabase.BGId bGId)
    {
        Init(TBL_Item.GetEntity(bGId));
    }

    private void Init(TBL_Item tBL_Item)
    {
        this.id = tBL_Item.Index;
        this.name = tBL_Item.name;
        this.itemDescription = tBL_Item.Description;
        this.Icon = tBL_Item.Icon;
        this.type = tBL_Item.ItemType;
        this.cropType = tBL_Item.CropType;
        this.growTime = tBL_Item.GrowTime;
        this.sellingPrice = tBL_Item.SellingPrice;
        this.purchasePrice = tBL_Item.PurchasePrice;
        this.minDropCount = tBL_Item.MinDropCount;
        this.maxDropCount = tBL_Item.MaxDropCount;
        this.minInventoryCount = tBL_Item.MinInventoryCount;
        this.maxInventoryCount = tBL_Item.MaxInventoryCount;
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
