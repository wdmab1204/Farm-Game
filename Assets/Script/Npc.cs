using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Inventory
{
    public NpcScriptableObject npcSO;
    public GameObject npcUI;
    private bool firstInput = false;
    private void Awake()
    {
        list = new List<Item>();
    }

    private void Start()
    {
        npcUI.SetActive(false);
        foreach (ItemScriptableObject itemSO in npcSO.items)
        {
            Item item = ItemManager.Instanse.GetItem(itemSO.id);
            list.Add(item);
        }
    }

    public void On()
    {
        npcUI.SetActive(true);
        for(int i=0; i<list.Count; i++)
        {
            slots[i] = npcUI.transform.GetChild(i).GetComponent<ShopSlot>();
            ((ShopSlot)slots[i]).SetSlot(list[i]);
        }
    }

    protected override void SelectSlot()
    {
        Vector2 v1 = new Vector2(1.0f, 1.0f);
        Vector2 v2 = new Vector2(1.05f, 1.05f);
        for(int i=0; i<slots.Length; i++)
        {
            if(i==index) slots[i].transform.localScale = v2;
            else slots[i].transform.localScale = v1;
        }
        
    }

    public new void ScrollControl(float scroll, int max = 9)
    {
        base.ScrollControl(scroll, max);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!firstInput) // 한번의 키입력으로 여러기능이 동시에 켜지지않기 위한 조치
            {
                firstInput = true;
                return;
            }
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Item item = GetItem();
            int sellingPrice = item.sellingPrice;
            bool b = player.money.SpendMoney(sellingPrice);

            if (b) //돈이 부족하지않다면
            {
                player.inventory.Add(item);
            }
        }
    }

    public void Off()
    {
        npcUI.SetActive(false);
    }
}
