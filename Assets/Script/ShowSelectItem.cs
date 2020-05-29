using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowSelectItem : MonoBehaviour
{

    public Tilemap tilemap;
    public GameObject selectItem;
    [ColorUsage(true)]
    public Color selectItemColor;
    public GameObject[] crops;
    Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        
    }

    private void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);

        Debug.Log("working");

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector3.zero);

        foreach(RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (this.tilemap = hit.transform.GetComponent<Tilemap>())
            {
                this.tilemap.RefreshAllTiles();

                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;

                Vector3Int v3Int = new Vector3Int(x, y, 0);

                if (Vector3.Distance(player.transform.position, v3Int) <= 3f)
                {
                    try
                    {
                        selectItem.GetComponent<SpriteRenderer>().sprite = player.GetSelectItem().GetIcon();
                    }
                    catch (NullReferenceException)
                    {
                        Debug.Log("Null");
                        selectItem.GetComponent<SpriteRenderer>().sprite = null;
                    }

                    selectItem.transform.position = v3Int;
                    Color color = selectItem.GetComponent<SpriteRenderer>().color;
                    color.a = 0.5f;
                    selectItem.GetComponent<SpriteRenderer>().color = color;
                }
                else
                {
                    selectItem.GetComponent<SpriteRenderer>().sprite = null;
                }

            }
        }


    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);



        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector3.zero);
        bool isCrop = false;
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Crop"))
            {
                isCrop = true;
                break;
            }
        }

        if (!isCrop)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (this.tilemap.Equals(hit.transform.GetComponent<Tilemap>()))
                {
                    Item item = player.GetSelectItem();
                    if (item == null) return;
                    if (item.type == Item.ItemType.use)
                    {
                        this.tilemap.RefreshAllTiles();

                        int x, y;
                        x = this.tilemap.WorldToCell(ray.origin).x;
                        y = this.tilemap.WorldToCell(ray.origin).y;

                        Vector3Int v3Int = new Vector3Int(x, y, 0);

                        int id = item.id;
                        GameObject obj = Instantiate(crops[id - 100]);
                        obj.transform.position = v3Int;

                    }

                }
            }
        }
        else
        {
            Debug.Log("No!!");
        }
        

    }

    private void OnMouseExit()
    {
        try
        {
            this.tilemap.RefreshAllTiles();
        }
        catch (MissingComponentException) { }
        

    }
}
