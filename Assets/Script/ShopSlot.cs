using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : Slot
{
    // Start is called before the first frame update
    private void Awake()
    {

    }

    // Update is called once per frame
    public new void SetSlot(Item item)
    {
        base.SetSlot(item);
        text.text = item.sellingPrice.ToString() + "골드";
    }
}
