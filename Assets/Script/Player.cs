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

    public GameObject[] crops;
    public Grid grid;
    private bool inGround = false;

    void Awake()
    {
        ac = GetComponent<Animator>();
    }


    private void Start()
    {
        JsonHelper.LoadJson();
    }

    void Update()
    {
        Moving();
        inventory.ScrollControl(Input.GetAxis("Mouse ScrollWheel"));
        if (Input.GetKeyDown("space")) inventory.UseItem(inGround, grid, transform);
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
