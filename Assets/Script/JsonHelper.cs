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

    // Start is called before the first frame update

    [ContextMenu("AddItem")]
    public void AddItem()
    {
        Item item = new Item(itemobj);
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //player.inventory.Add(item);
        SaveJson();
    }

    [ContextMenu("SaveJson")]
    public static void SaveJson()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        string jdata = JsonUtility.ToJson(new Serialization<Item>(player.inventory.list), prettyPrint: true);
        File.WriteAllText(Application.streamingAssetsPath + "/inventory.json", jdata);
    }

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
