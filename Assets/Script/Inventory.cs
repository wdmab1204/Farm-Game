using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    protected int index;
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

    /// <summary>
    /// 선택된 슬롯의 아이템을 획득합니다.
    /// </summary>
    /// <returns>현재 선택된 아이템을 반환합니다.</returns>
    public Item GetItem()
    {
        return slots[index].item;
    }

    /// <summary>
    /// 선택슬롯을 기준으로 아이템을 삭제합니다.
    /// </summary>
    public void RemoveItem()
    {
        slots[index].RemoveItem();
    }

    /// <summary>
    /// list에 아이템을 추가합니다. 중복이 있을 시 개수를 증가시킵니다.
    /// </summary>
    /// <param name="item"></param>
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

    /// <summary>
    /// 변수를 통해 숫자키 입력 또는 휠 입력으로 인벤토리상의 아이템선택을 도와주는 함수입니다.
    /// </summary>
    /// <param name="scroll">-1이면 왼쪽, 1이면 오른쪽으로 선택칸이 이동합니다. 휠입력을 받을 시 GetAxis("Mouse ScrollWheel")을 호출하면됩니다.</param>
    /// <param name="max">스크롤의 최대이동위치를 나타냅니다. 기본값은 9입니다.</param>
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

        SelectSlot();
    }

    virtual protected void SelectSlot()
    {
        selectSign.transform.position = slots[index].transform.position;
    }
    /// <summary>
    /// list변수를 기준으로 ui상에 아이템을 추가합니다, 매개변수는 필요없습니다.
    /// </summary>
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
                    slots[i].item = null;
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

    /// <summary>
    /// 현재 슬롯의 아이템들을 기준으로 리스트를 업데이트합니다, 기존의 리스트 데이터는 사라집니다.
    /// </summary>
    /// <returns>최종적으로 업데이트된 list를 반환합니다.</returns>
    public List<Item> ListUpdate()
    {
        List<Item> list = new List<Item>();
        for(int i=0; i<slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                Item item = slots[i].item;
                list.Add(item);
            }
            
        }
        this.list = new List<Item>(list);
        return this.list;
    }

    /// <summary>
    /// 아이템을 사용합니다.
    /// </summary>
    /// <param name="inGround">플레이어가 경작지 안에 있는지에 대한 변수입니다. 안에있다면 true, 아니라면 false입니다.</param>
    /// <param name="grid">grid를 통해 타일간격에 맞게 클론(작물 등)을 생성합니다.</param>
    /// <param name="playerPosition">플레이어의 위치값을 통해 클론(작물 등)을 생성합니다.</param>
    public void UseItem(bool inGround, Grid grid, Vector3 playerPosition)
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

        Vector3Int tilePos = grid.WorldToCell(playerPosition);
        Debug.Log((Vector3)tilePos);
        Collider2D[] hits = Physics2D.OverlapBoxAll(playerPosition, new Vector2(0.5f, 0.5f), 0);

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
