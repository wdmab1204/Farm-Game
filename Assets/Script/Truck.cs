using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    public int maxCount;
    private int currentCount;

    public Text stateText;
    private GameObject textObject;
    private Transform target;
    private new Camera camera;
    private Player player;
    private bool findPlayer;
    private List<Item> list;
    private StringBuilder sb;

    private void Awake()
    {
        currentCount = 0;
        stateText.text = "0 / " + maxCount.ToString();
        target = GetComponent<Transform>();
        camera = Camera.main;
        player = Player.Instance;
        textObject = stateText.gameObject;
        list = new List<Item>();
        sb = new StringBuilder();
    }

    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);
        float x = screenPos.x;
        stateText.transform.position = new Vector3(x, screenPos.y, stateText.transform.position.z);
    }

    private void FixedUpdate()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, new Vector2(5f, 5f), 0);
        findPlayer = false;
        foreach(Collider2D col in cols)
        {
            if (col.transform.Equals(player.transform))
            {
                findPlayer = true;
                break;
            }
        }
        if (findPlayer) textObject.SetActive(true);
        else textObject.SetActive(false);
    }

    public void SetCrop(Item crop)
    {
        if(crop.type == Item.ItemType.etc && crop.cropType != Item.CropType.Nothing)
        {
            if (currentCount + crop.count <= maxCount)
            {
                currentCount += crop.count;
                stateText.text = CombineString(currentCount.ToString(), " / ", maxCount.ToString());
                list.Add(crop);
                player.inventory.Add(crop, -crop.count); //delete

                
            }
        }
    }

    private string CombineString(params string[] arr)
    {
        sb.Clear();
        foreach(string str in arr)
        {
            sb.Append(str);
        }
        return sb.ToString();
    }


}
