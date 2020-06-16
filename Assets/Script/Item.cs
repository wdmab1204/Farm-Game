﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string itemDescription;
    public int count;
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
                    throw new System.NullReferenceException();
                }
            }
            return icon;
        }
    }
    public ItemType type;

    public enum ItemType
    {
        equip, //장비
        use, //소비
        etc, //기타
        tool //도구
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

}
