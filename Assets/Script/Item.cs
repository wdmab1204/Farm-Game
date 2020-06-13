using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string itemDescription;
    public int count;
    private Sprite icon;
    public ItemType type;

    public enum ItemType
    {
        equip,
        use,
        etc,
        tool
    }

    public Item(int id, string name, string itemDescription, ItemType type, int count = 1) {
        this.id = id;
        this.name = name;
        this.itemDescription = itemDescription;
        this.type = type;
        this.count = count;
    }

    public Item(ItemScriptableObject obj)
    {
        this.id = obj.id;
        this.name = obj.name;
        this.itemDescription = obj.description;
        this.type = obj.type;
        this.count = obj.count;
        SetIcon();
    }

    public void SetIcon()
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
            throw new System.NullReferenceException();
        }
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public object Copy()
    {
        return this.MemberwiseClone();
    }

}
