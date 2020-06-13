using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : List<Item>
{
    private int index;
    private readonly KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    public Inventory(List<Item> list)
    {
        foreach(Item item in list)
        {
            this.Add(item);
        }
    }

    public Item GetItem()
    {
        return this[index];
    }

    public void RemoveItem()
    {
        Remove(this[index]);
    }

    public new void Add(Item item)
    {
        
        base.Add(item);
    }

    public int ScrollControl(float scroll, int max = 9)
    {

        if (scroll > 0) index -= 1;
        else if (scroll < 0) index += 1;

        if (index < 0) index = 0;
        else if (index >= max) index = max - 1;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                index = i;
            }
        }

        //selectSign.transform.position = slots[inventoryIndex].transform.position;
        return index;
    }

    public void Refresh(ref GameObject[] slots)
    {
        if (Count <= 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].GetComponent<Image>().sprite = null;
                Color slotColor = slots[i].GetComponent<Image>().color;
                slotColor.a = 0f;
                slots[i].GetComponent<Image>().color = slotColor;
            }
        }
        else
        {

            for (int i = 0; i < slots.Length; i++)
            {
                Sprite sprite;
                float colorAlphaValue = 1f;
                if (Count <= i)
                {
                    sprite = null;
                    colorAlphaValue = 0f;
                }
                else
                {
                    sprite = this[i].GetIcon();
                }

                slots[i].GetComponent<Image>().sprite = sprite;
                Color slotColor = slots[i].GetComponent<Image>().color;
                slotColor.a = colorAlphaValue;
                slots[i].GetComponent<Image>().color = slotColor;
            }
        }

    }
}
