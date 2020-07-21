﻿using UnityEngine;
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

    private void Awake()
    {
        currentCount = 0;
        stateText.text = "0 / " + maxCount.ToString();
        target = GetComponent<Transform>();
        camera = Camera.main;
        player = Player.Instance;
        textObject = stateText.gameObject;
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
                stateText.text = currentCount.ToString() + " / " + maxCount.ToString();
                player.inventory.Add(crop, -crop.count); //delete
            }
        }
    }


}
