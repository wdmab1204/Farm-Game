using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float speed;
    public bool diagonalMoving;

    private Animator ac;
    private bool playerMoving;
    private bool upDownMoving;
    private bool leftRightMoving;
    private Vector2 cureentMove;
    private Vector2 lastMove;

    [Header("Item and Inventory")]
    //public GameObject[] slots;
    public Inventory inventory;
    public Text debugText;
    //public GameObject selectSign;

    public GameObject[] crops;
    public Grid grid;
    private bool inGround = false;
    private JsonHelper jsonHelper;

    void Awake()
    {
        ac = GetComponent<Animator>();
        jsonHelper = FindObjectOfType<JsonHelper>();
        jsonHelper.LoadJson();
    }


    void Update()
    {
        Moving();
        //index = inventory.ScrollControl(Input.GetAxis("Mouse ScrollWheel"));
        //selectSign.transform.position = slots[index].transform.position;
        if (Input.GetKeyDown("space")) UseItem();
    }

    void Moving()
    {
        playerMoving = false;
        upDownMoving = (Input.GetAxisRaw("Vertical") != 0f) ? true : false;
        leftRightMoving = (Input.GetAxisRaw("Horizontal") != 0f) ? true : false;
        //좌 우
        if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f)
        {
            if (!upDownMoving || diagonalMoving)
            {
                cureentMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f, 0f));
                playerMoving = true;
                leftRightMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }
        }

        //위 아래
        if (Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
        {
            if (!leftRightMoving || diagonalMoving)
            {
                cureentMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
                transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
                playerMoving = true;
                upDownMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

        }

        ac.SetFloat("MoveX", cureentMove.x);
        ac.SetFloat("MoveY", cureentMove.y);
        ac.SetBool("PlayerMoving", playerMoving);

        ac.SetFloat("LastMoveX", lastMove.x);
        ac.SetFloat("LastMoveY", lastMove.y);
    }

    private void UseItem()
    {
        Item item;
        try
        {
            item = inventory.GetItem();
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }

        Vector3Int tilePos = grid.WorldToCell(transform.position);
        Debug.Log((Vector3)tilePos);
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, 0.5f), 0);

        switch (item.type)
        {
            case Item.ItemType.use:
                if (inGround)
                {
                    bool isEmpty = false;
                    foreach(Collider2D hit in hits)
                    {
                        if (hit.gameObject.layer == LayerMask.NameToLayer("Cultivated Ground"))
                        {
                            isEmpty = true;
                        }
                        else if (hit.gameObject.CompareTag("Player")) continue;
                        else {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        GameObject obj = Instantiate(crops[item.id - 100]);
                        obj.transform.position = tilePos;
                        obj.GetComponentInChildren<GrowSystem>().item = item;

                        inventory.GetItem().count -= 1;
                        if (inventory.GetItem().count <= 0)
                        {
                            inventory.RemoveItem();
                        }
                        inventory.Refresh();
                    }


                }
                
                
                break;
            case Item.ItemType.equip:
                break;
            case Item.ItemType.etc:
                break;
            case Item.ItemType.tool:
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.CompareTag("Crop"))
                    {
                        GrowSystem gs = hit.gameObject.GetComponentInChildren<GrowSystem>();
                        bool b = gs.Harvest();
                        if (b) Destroy(gs.transform.parent.gameObject);
                        break;
                    }
                }
                
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Cultivated Ground"))
        {
            inGround = true;
            Debug.Log("그라운드 안");
        }
        if (collision.gameObject.CompareTag("DropItem"))
        {
            Item item = collision.gameObject.GetComponent<DropItem>().item;
            inventory.Add(item);
            inventory.Refresh();
            Debug.Log("아이템 습득" + item.name);
            Destroy(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cultivated Ground"))
        {
            inGround = false;
            Debug.Log("그라운드 밖");
        }

    }

}
