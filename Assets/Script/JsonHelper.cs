using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

class Serialization
{
    [SerializeField]
    Item[] items;
    [SerializeField]
    int money;
    public Item[] GetItems() { return items; }
    public int GetMoney() { return money; }
    public Serialization(Item[] items, int money)
    {
        this.items = items;
        this.money = money;
    }
}

public class JsonHelper : Singleton<JsonHelper>
{
    public Text debugText;

    public ItemScriptableObject itemobj;
    private Player player;
    public static bool isLoaded = false;

    private void Awake()
    {
        player = Player.Instance;
    }

    /// <summary>
    /// Inspector창에 참조되어있는 ItemScriptableObject를 아이템화시켜 인벤토리에 추가합니다.
    /// </summary>
    [ContextMenu("AddItem")]
    public void AddItem()
    {
        Item item = new Item(itemobj);
        player.inventory.Add(item);
        SaveJson();
    }

    /// <summary>
    /// 플레이어 인벤토리의 아이템 데이터들을 json으로 변환하여 지정된 경로에 저장합니다.
    /// </summary>
    [ContextMenu("SaveJson")]
    public void SaveJson()
    {
        List<Item> inventoryList = player.inventory.ListUpdate();
        Item[] items = inventoryList.ToArray();
        string jdata = JsonUtility.ToJson(new Serialization(items,777), prettyPrint: true);
        File.WriteAllText(Application.streamingAssetsPath + "/inventory.json", jdata);
    }

    /// <summary>
    /// 정해진 경로에있는 json파일을통해 플레이어의 인벤토리에 데이터를 추가합니다.
    /// </summary>
    public void LoadJson()
    {

        try
        {
            string jdata = File.ReadAllText(Application.streamingAssetsPath + "/inventory.json");
            Serialization s = JsonUtility.FromJson<Serialization>(jdata);
            //Item[] items = s.GetItems();
            //player.inventory.list = new List<Item>(items);
            GameData.items = s.GetItems();
            GameData.money = s.GetMoney();
            isLoaded = true;
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }

        if (player.inventory != null) player.inventory.Refresh();
        
    }

}
