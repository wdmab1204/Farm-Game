using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }
    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

public class JsonHelper : MonoBehaviour
{
    public Text debugText;

    public ItemScriptableObject itemobj;

    /// <summary>
    /// Inspector창에 참조되어있는 ItemScriptableObject를 아이템화시켜 인벤토리에 추가합니다.
    /// </summary>
    [ContextMenu("AddItem")]
    public void AddItem()
    {
        Item item = new Item(itemobj);
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.inventory.Refresh(item);
        SaveJson();
    }

    /// <summary>
    /// 플레이어 인벤토리의 아이템 데이터들을 json으로 변환하여 지정된 경로에 저장합니다.
    /// </summary>
    [ContextMenu("SaveJson")]
    public static void SaveJson()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        List<Item> inventoryList = player.inventory.ListUpdate();
        string jdata = JsonUtility.ToJson(new Serialization<Item>(inventoryList), prettyPrint: true);
        File.WriteAllText(Application.streamingAssetsPath + "/inventory.json", jdata);
    }

    /// <summary>
    /// 정해진 경로에있는 json파일을통해 플레이어의 인벤토리에 데이터를 추가합니다.
    /// </summary>
    [ContextMenu("LoadJson")]
    public static void LoadJson()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        try
        {
            string jdata = File.ReadAllText(Application.streamingAssetsPath + "/inventory.json");
            List<Item> list = JsonUtility.FromJson<Serialization<Item>>(jdata).ToList();
            player.inventory.list = list;
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        
        player.inventory.Refresh();
        
    }
}
