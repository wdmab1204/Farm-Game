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
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Animation")]
    private Animator ac;
    private bool playerMoving;
    private bool upDownMoving;
    private bool leftRightMoving;
    private Vector2 cureentMove;
    private Vector2 lastMove;

    [Header("Inventory")]
    public Inventory inventory;
    public Text debugText;
    public Money money;

    public Grid grid;
    public LayerMask layerMask;
    private bool inGround = false;
    private bool showUI = false;
    private Npc npc;

    void Awake()
    {
        ac = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        JsonHelper.LoadJson();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movement = new Vector2(h, v);
        if (npc == null)
        {
            inventory.ScrollControl(Input.GetAxis("Mouse ScrollWheel"));
        }
        else
        {
            npc.ScrollControl(Input.GetKeyDown(KeyCode.RightArrow) ? -1 : Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0, 4);
        }
        
        if (Input.GetKeyDown("space"))
        {
            if (npc != null)
            {
                npc.On();
                showUI = true;
            }
            else
            {
                inventory.UseItem(inGround, grid, transform.position);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showUI = false;
            npc.Off();
        }
    }

    private void FixedUpdate()
    {
        if(!showUI) Moving(movement);
        npc = GetNpc();
        Debug.Log(npc);
    }

    private Npc GetNpc()
    {
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, transform.localScale, 0, 1<<LayerMask.NameToLayer("Npc"));
        if (hitCollider != null)
        {
            Debug.Log(hitCollider.name);
            return hitCollider.GetComponent<Npc>();
        }
        return null;
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
                cureentMove = new Vector2(direction.x, 0f);
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
                cureentMove = new Vector2(0f, direction.y);
                rb.velocity = direction * speed;
                playerMoving = true;
                upDownMoving = true;
                lastMove = new Vector2(0f, direction.y);
            }
        }

        if (!playerMoving) rb.velocity = Vector2.zero;

        ac.SetFloat("MoveX", cureentMove.x);
        ac.SetFloat("MoveY", cureentMove.y);
        ac.SetBool("PlayerMoving", playerMoving);

        ac.SetFloat("LastMoveX", lastMove.x);
        ac.SetFloat("LastMoveY", lastMove.y);
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
            //inventory.Add(item);
            inventory.Refresh(item);
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
