using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed;
    public bool diagonalMoving;

    private Animator ac;
    private bool playerMoving;
    private bool upDownMoving;
    private bool leftRightMoving;
    private Vector2 cureentMove;
    private Vector2 lastMove;

    void Awake()
    {
        ac = GetComponent<Animator>();
    }

    void Update()
    {
        //if (First_Arrow == 0 && Input.GetKey(KeyCode.LeftArrow)) First_Arrow = 1;
        //if (First_Arrow == 0 && Input.GetKey(KeyCode.RightArrow)) First_Arrow = 2;
        //if (First_Arrow == 0 && Input.GetKey(KeyCode.UpArrow)) First_Arrow = 3;
        //if (First_Arrow == 0 && Input.GetKey(KeyCode.DownArrow)) First_Arrow = 4;

        //if (First_Arrow == 1 && Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
        //    ac.SetTrigger("LeftMove");
        //    Debug.Log("i am left");
        //}
        //if (First_Arrow == 2 && Input.GetKey(KeyCode.RightArrow)) transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
        //if (First_Arrow == 3 && Input.GetKey(KeyCode.UpArrow)) transform.Translate(new Vector2(0, speed) * Time.deltaTime);
        //if (First_Arrow == 4 && Input.GetKey(KeyCode.DownArrow)) transform.Translate(new Vector2(0, -speed) * Time.deltaTime);

        //if ((First_Arrow == 1 && Input.GetKeyUp(KeyCode.LeftArrow))
        //    || (First_Arrow == 2 && Input.GetKeyUp(KeyCode.RightArrow))
        //    || (First_Arrow == 3 && Input.GetKeyUp(KeyCode.UpArrow))
        //    || (First_Arrow == 4 && Input.GetKeyUp(KeyCode.DownArrow)))
        //{
        //    First_Arrow = 0;
        //    ac.SetTrigger("Idle");
        //    Debug.Log("i am idle");
        //}
        playerMoving = false;
        upDownMoving = (Input.GetAxisRaw("Vertical") != 0f) ? true : false;
        leftRightMoving = (Input.GetAxisRaw("Horizontal") != 0f) ? true : false;
        //좌 우
        if (Input.GetAxisRaw("Horizontal")>0f || Input.GetAxisRaw("Horizontal") < 0f)
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
}
