using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instanse
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<ItemManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("ItemManager").AddComponent<ItemManager>();
                    instance = newSingleton;
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

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
        Debug.Log(id);
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
            Debug.LogWarning("Item not Found");
        }
        return item;
    }
}
