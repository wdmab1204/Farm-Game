using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : List<Item>
{
    private int inventoryIndex;
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
        return this[inventoryIndex];
    }

    public void RemoveItem()
    {
        Remove(this[inventoryIndex]);
    }

    public int ScrollControl(float scroll, int max = 9)
    {

        if (scroll > 0) inventoryIndex -= 1;
        else if (scroll < 0) inventoryIndex += 1;

        if (inventoryIndex < 0) inventoryIndex = 0;
        else if (inventoryIndex >= max) inventoryIndex = max - 1;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                inventoryIndex = i;
            }
        }

        //selectSign.transform.position = slots[inventoryIndex].transform.position;
        return inventoryIndex;
    }
}
