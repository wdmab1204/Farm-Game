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
    public GameObject[] slots;
    [HideInInspector]
    public List<Item> inventory;
    public Text debugText;
    public GameObject selectSign;
    private int inventoryIndex = 0;
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    public GameObject[] crops;
    public Grid grid;
    private bool inGround =false;


    void Awake()
    {
        ac = GetComponent<Animator>();
        
    }

    void Update()
    {
        Moving();
        ScrollControl();
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

    public void Refresh()
    {
        foreach(Item item in inventory)
        {
            Sprite sprite = item.GetIcon();
            debugText.text = sprite.name;
            slots[0].GetComponent<Image>().sprite = sprite;
            Color slotColor = slots[0].GetComponent<Image>().color;
            slotColor.a = 1f;
            slots[0].GetComponent<Image>().color = slotColor;
        }
        
    }

    public Item GetSelectItem()
    {
        try
        {
            return inventory[inventoryIndex];
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
        
    }

    private void ScrollControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0) inventoryIndex -= 1;
        else if (scroll < 0) inventoryIndex += 1;

        if (inventoryIndex < 0) inventoryIndex = 0;
        else if (inventoryIndex >= slots.Length) inventoryIndex = slots.Length - 1;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                inventoryIndex = i;
            }
        }

        selectSign.transform.position = slots[inventoryIndex].transform.position;
        
        
    }

    private void UseItem()
    {
        Item item = inventory[inventoryIndex];
        switch (item.type)
        {
            case Item.ItemType.use:
                if (inGround)
                {
                    Vector3Int tilePos = grid.WorldToCell(transform.position);
                    Debug.Log((Vector3)tilePos);
                    Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.0f, 1.0f), 0);

                    bool isEmpty = false;
                    foreach(Collider2D hit in hits)
                    {
                        if (hit.gameObject.layer == LayerMask.NameToLayer("Cultivated Ground"))
                        {
                            isEmpty = true;
                        }
                        else if (hit.gameObject.CompareTag("Player")) continue;
                        else
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        GameObject obj = Instantiate(crops[item.id - 100]);
                        obj.transform.position = tilePos;
                    }


                }
                
                
                break;
            case Item.ItemType.equip:
                break;
            case Item.ItemType.etc:
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
