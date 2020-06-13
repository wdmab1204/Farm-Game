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
    Player player;

    public ItemScriptableObject itemobj;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        LoadJson();
    }

    [ContextMenu("AddItem")]
    public void AddItem()
    {
        Item item = new Item(itemobj);
        player.inventory.Add(item);
        SaveJson();
    }

    [ContextMenu("SaveJson")]
    public void SaveJson()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Item>(player.inventory), prettyPrint: true);
        File.WriteAllText(Application.streamingAssetsPath + "/inventory.json", jdata);
    }

    [ContextMenu("LoadJson")]
    public void LoadJson()
    {
        try
        {
            string jdata = File.ReadAllText(Application.streamingAssetsPath + "/inventory.json");
            player.inventory = new Inventory(JsonUtility.FromJson<Serialization<Item>>(jdata).ToList());
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        
        
        for(int i=0; i<player.inventory.Count; i++)
        {
            player.inventory[i].SetIcon();
        }
        debugText.text = player.inventory[0].name;
        player.inventory.Refresh(ref player.slots);
        
    }
}
