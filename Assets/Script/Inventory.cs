using System;
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
    public Slot[] slots;
    public GameObject selectSign;


    public Item GetItem()
    {
        return slots[index].item;
    }

    public void RemoveItem()
    {
        slots[index].RemoveItem();
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

    public void ScrollControl(float scroll, int max = 9)
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

        selectSign.transform.position = slots[index].transform.position;
    }

    public void Refresh()
    {
        if (list.Count <= 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Img.sprite = null;
                slots[i].item = null;
                Color slotColor = slots[i].Img.color;
                slotColor.a = 0f;
                slots[i].Img.color = slotColor;

                slots[i].text.text = "";
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
                    slots[i].item = list[i];
                    if (list[i].type != Item.ItemType.tool)
                        countString = list[i].count.ToString();
                }

                slots[i].Img.sprite = sprite;
                Color slotColor = slots[i].Img.color;
                slotColor.a = colorAlphaValue;
                slots[i].Img.color = slotColor;
                slots[i].text.text = countString;
            }
        }

    }

    /// <summary>
    /// 아이템을 ui상 인벤토리에 추가합니다.
    /// </summary>
    /// <param name="item">추가할 아이템</param>
    /// <param name="count">아이템의 증감수치를 나타냅니다, count값만큼 기존 아이템의 개수를 더하거나 뺍니다.</param>
    public void Refresh(Item item, int count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].SetSlot(item);
                Debug.Log(slots[i].item);
                break;
            }
            else if (slots[i].item.id == item.id)
            {

                slots[i].text.text = (slots[i].item.count + count).ToString();
                slots[i].item.count = slots[i].item.count + count;
                if (slots[i].item.count <= 0)
                {
                    RemoveItem();
                }
                Debug.Log(slots[i].item);
                break;
            }
        }
    }

    public void ListUpdate()
    {
        List<Item> list = new List<Item>();
        for(int i=0; i<slots.Length; i++)
        {
            Item item = slots[i].item;
            list.Add(item);
        }
        this.list = new List<Item>(list);
    }

    public void UseItem(bool inGround, Grid grid, Transform playerTransform)
    {
        Item item;
        try
        {
            item = (Item)GetItem().Clone();
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }

        Vector3Int tilePos = grid.WorldToCell(playerTransform.position);
        Debug.Log((Vector3)tilePos);
        Collider2D[] hits = Physics2D.OverlapBoxAll(playerTransform.position, new Vector2(0.5f, 0.5f), 0);

        switch (item.type)
        {
            case Item.ItemType.use:
                if (inGround)
                {
                    bool isEmpty = false;
                    foreach (Collider2D hit in hits)
                    {
                        if (hit.gameObject.layer == LayerMask.NameToLayer("Cultivated Ground"))
                        {
                            isEmpty = true;
                        }
                        else if (hit.gameObject.CompareTag("Player")) continue;
                        else
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/" + item.id));
                        obj.transform.position = tilePos;
                        obj.GetComponentInChildren<GrowSystem>().item = item;

                        //item.count = -1;
                        Refresh(item, -1);
                    }
                }


                break;
            case Item.ItemType.equip:
                break;
            case Item.ItemType.etc:
                break;
            case Item.ItemType.tool:
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.CompareTag("Crop"))
                    {
                        GrowSystem gs = hit.gameObject.GetComponentInChildren<GrowSystem>();
                        bool b = gs.Harvest();
                        if (b) Destroy(gs.transform.parent.gameObject);
                        break;
                    }
                }

                break;
            default:
                break;
        }
    }
}
