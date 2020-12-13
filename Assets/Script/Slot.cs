using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image Img;
    public Text text;

    private void Awake()
    {

    }

    /// <summary>
    /// 아이템을 슬롯에 추가합니다.
    /// </summary>
    /// <param name="item">슬롯에 추가할 아이템입니다. 이 변수를 기준으로 슬롯에 데이터를 추가합니다.</param>
    public void SetSlot(Item item)
    {
        string countString = "";
        if (item.type != ItemType.equip)
            countString = item.count.ToString();
        Img.sprite = item.Icon;
        this.item = item;
        Color slotColor = Img.color;
        slotColor.a = 1f;
        Img.color = slotColor;
        text.text = countString;
    }


    /// <summary>
    /// 슬롯에서 선택칸에 있는 아이템을 삭제합니다.
    /// </summary>
    /// <returns>삭제된 아이템을 반환합니다.</returns>
    public Item RemoveItem()
    {
        Item item = this.item;
        this.item = null;
        Img.sprite = null;
        Color color = Img.color;
        color.a = 0f;
        Img.color = color;
        text.text = "";


        return item;
    }

}
