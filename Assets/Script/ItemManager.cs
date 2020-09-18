using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{

    [SerializeField]
    private ItemScriptableObject[] items = null;

    private void Awake()
    {
        var objs = FindObjectsOfType<ItemManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }


    public Item GetItem(int id)
    {
        Item item = null;
        foreach(ItemScriptableObject itemobj in items)
        {
            if (id.Equals(itemobj.id))
            {
                item = new Item(itemobj);
                break;
            }
        }
        if (item == null)
        {
            Debug.LogWarning("Item not Found, id : " + id);
        }
        return item;
    }
}
