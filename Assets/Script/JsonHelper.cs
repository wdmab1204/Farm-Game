using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

class Serialization
{
    [SerializeField]
    List<Item> target;
    [SerializeField]
    int money;
    public List<Item> GetList() { return target; }
    public int GetMoney() { return money; }
    public Serialization(List<Item> target, int money)
    {
        this.target = target;
        this.money = money;
    }
}

public class JsonHelper : Singleton<JsonHelper>
{
    public Text debugText;

    public ItemScriptableObject itemobj;
    private Player player;

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
        string jdata = JsonUtility.ToJson(new Serialization(inventoryList,777), prettyPrint: true);
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
            player.inventory.list = s.GetList();
            player.money.SetMoney(s.GetMoney());
            Debug.Log(s.GetMoney());
            //int coin = s.ToMoney();
            //player.money.money = coin;
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        
        player.inventory.Refresh();
        
    }

}
