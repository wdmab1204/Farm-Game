using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Tilemaps;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class Player : Singleton<Player>
{
    [Header("Move")]
    public float speed;
    public bool diagonalMoving;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Animation")]
    private Animator ac;
    private bool playerMoving;
    private bool upDownMoving;
    private bool leftRightMoving;
    private Vector2 current;
    private Vector2 lastMove;

    [Header("Inventory")]
    public Inventory inventory;
    public Text debugText;
    public Money money;

    [Header("ETC")]
    private Npc npc;
    private Truck truck;
    private CultivatedGroundContract cgc;
    private Camera mainCamera;
    private Vector3 offset;

    void Awake()
    {
        ac = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        offset = transform.position - mainCamera.transform.position;
        if (inventory != null) inventory.list = new List<Item>(GameData.items);
    }


    private void Start()
    {
        if(JsonHelper.isLoaded == false)
        {
            JsonHelper.Instance.LoadJson();
        }
        if(money != null) money.LoadMoney();
        if (inventory != null) inventory.Refresh();
    }


    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movement = new Vector2(h, v);

        if (Npc.UIOnOff)
        {
            npc.ScrollControl(Input.GetKeyDown(KeyCode.RightArrow) ? -1 : Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0, max: 4);
        }
        else if (CultivatedGroundContract.UIOnOff)
        {
            cgc.ScrollControl(Input.GetKey(KeyCode.RightArrow) ? -1 : Input.GetKey(KeyCode.LeftArrow) ? 1 : 0, max: 5);
        }
        else
        {
            if (inventory != null)
                inventory.ScrollControl(Input.GetAxis("Mouse ScrollWheel"));
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && !Npc.UIOnOff)
        {
            inventory.UseItem(transform.position, GetDirection());
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (npc != null)
            {
                npc.On();
            }
            if(cgc != null)
            {
                cgc.On();
            }
            if(truck != null)
            {
                Item item = inventory.GetItem();
                if(item != null) truck.SetCrop(item);
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (npc != null) npc.Off();
            if (cgc != null) cgc.Off();
        }
    }

    private void FixedUpdate()
    {
        if(!Npc.UIOnOff && !CultivatedGroundContract.UIOnOff) Moving(movement);
        //npc = GetObject<Npc>("Npc");
        //truck = GetObject<Truck>("Truck");
    }


    private T GetObject<T>(string layerName = "")
    {
        Collider2D hitCollider;

        if (!string.IsNullOrEmpty(layerName))
            hitCollider = Physics2D.OverlapBox(transform.position, transform.localScale, 0, 1 << LayerMask.NameToLayer(layerName));
        else
            hitCollider = Physics2D.OverlapBox(transform.position, transform.localScale, 0);


        if (hitCollider != null)
        {
            Debug.Log(hitCollider.name);
            return hitCollider.GetComponent<T>();
        }


        return default(T);
    }

    private void Moving(Vector2 direction)
    {
        playerMoving = false;
        upDownMoving = (direction.y != 0f) ? true : false;
        leftRightMoving = (direction.x != 0f) ? true : false;
        //좌 우
        if (direction.x > 0f || direction.x < 0f)
        {
            if (!upDownMoving || diagonalMoving)
            {
                current = new Vector2(direction.x, 0f);
                rb.velocity = direction * speed;
                playerMoving = true;
                leftRightMoving = true;
                lastMove = new Vector2(direction.x, 0f);
            }
        }

        //위 아래
        if (direction.y > 0f || direction.y < 0f)
        {
            if (!leftRightMoving || diagonalMoving)
            {
                current = new Vector2(0f, direction.y);
                rb.velocity = direction * speed;
                playerMoving = true;
                upDownMoving = true;
                lastMove = new Vector2(0f, direction.y);
            }
        }

        if (!playerMoving) rb.velocity = Vector2.zero;

        ac.SetFloat("MoveX", current.x);
        ac.SetFloat("MoveY", current.y);
        ac.SetBool("PlayerMoving", playerMoving);

        ac.SetFloat("LastMoveX", lastMove.x);
        ac.SetFloat("LastMoveY", lastMove.y);

        //Camera smooth moving
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, transform.position - offset, 0.1f);
    }

    private int GetDirection()
    {
        int direction = 0;
        if (current.x == 0f && current.y >= 0f) direction = 1;
        else if (current.x >= 0f && current.y == 0f) direction = 2;
        else if (current.x == 0f && current.y <= 0f) direction = 3;
        else if (current.x <= 0f && current.y == 0f) direction = 4;

        return direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DropItem"))
        {
            Item item = collision.gameObject.GetComponent<DropItem>().item;
            inventory.Add(item);
            Destroy(collision.gameObject);
        }

        cgc = collision.GetComponent<CultivatedGroundContract>();
        if(cgc!=null) Debug.Log(cgc.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cgc = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform t = collision.transform;
        if (t.CompareTag("Npc")) npc = collision.transform.GetComponent<Npc>();
        else if(t.CompareTag("Truck")) truck = collision.transform.GetComponent<Truck>();
        else if (t.CompareTag("Warp"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        npc = null;
        truck = null;
    }

}
