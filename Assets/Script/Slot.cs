using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Grid grid { get; set; }
    public Image Img { get; set; }
    public Text text { get; set; }

    private void Awake()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        Img = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    public void SetSlot(Item item)
    {
        string countString = "";
        if (item.type != Item.ItemType.tool)
            countString = item.count.ToString();
        Img.sprite = item.Icon;
        this.item = item;
        Color slotColor = Img.color;
        slotColor.a = 1f;
        Img.color = slotColor;
        text.text = countString;
    }

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
