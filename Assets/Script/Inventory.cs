using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
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
    public List<Item> list;
    public GameObject[] slots;
    public GameObject selectSign;


    private void Update()
    {
        index = ScrollControl(Input.GetAxis("Mouse ScrollWheel"));
        selectSign.transform.position = slots[index].transform.position;
    }

    public Item GetItem()
    {
        return list[index];
    }

    public void RemoveItem()
    {
        list.RemoveAt(index);
    }

    public void Add(Item item)
    {
        for(int i=0; i<list.Count; i++)
        {
            if(list[i].id == item.id)
            {
                list[i].count += item.count;
                return;
            }
        }
        list.Add(item);
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

        return index;
    }

    public void Refresh()
    {
        if (list.Count <= 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].GetComponent<Image>().sprite = null;
                Color slotColor = slots[i].GetComponent<Image>().color;
                slotColor.a = 0f;
                slots[i].GetComponent<Image>().color = slotColor;

                slots[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
        else
        {

            for (int i = 0; i < slots.Length; i++)
            {
                Sprite sprite;
                float colorAlphaValue = 1f;
                string countString = "";
                if (list.Count <= i)
                {
                    sprite = null;
                    colorAlphaValue = 0f;
                }
                else
                {
                    sprite = list[i].Icon;

                    if (list[i].type != Item.ItemType.tool)
                        countString = list[i].count.ToString();
                }

                slots[i].GetComponent<Image>().sprite = sprite;
                Color slotColor = slots[i].GetComponent<Image>().color;
                slotColor.a = colorAlphaValue;
                slots[i].GetComponent<Image>().color = slotColor;
                slots[i].transform.GetChild(0).GetComponent<Text>().text = countString;
            }
        }

    }
}
