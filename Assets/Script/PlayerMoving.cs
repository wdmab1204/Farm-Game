using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed;
    private int First_Arrow = 0;

    void Update()
    {
        if (First_Arrow == 0 && Input.GetKey(KeyCode.LeftArrow)) First_Arrow = 1;
        if (First_Arrow == 0 && Input.GetKey(KeyCode.RightArrow)) First_Arrow = 2;
        if (First_Arrow == 0 && Input.GetKey(KeyCode.UpArrow)) First_Arrow = 3;
        if (First_Arrow == 0 && Input.GetKey(KeyCode.DownArrow)) First_Arrow = 4;

        if (First_Arrow == 1 && Input.GetKey(KeyCode.LeftArrow)) transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
        if (First_Arrow == 2 && Input.GetKey(KeyCode.RightArrow)) transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
        if (First_Arrow == 3 && Input.GetKey(KeyCode.UpArrow)) transform.Translate(new Vector2(0, speed) * Time.deltaTime);
        if (First_Arrow == 4 && Input.GetKey(KeyCode.DownArrow)) transform.Translate(new Vector2(0, -speed) * Time.deltaTime);

        if (First_Arrow == 1 && Input.GetKeyUp(KeyCode.LeftArrow)) First_Arrow = 0;
        if (First_Arrow == 2 && Input.GetKeyUp(KeyCode.RightArrow)) First_Arrow = 0;
        if (First_Arrow == 3 && Input.GetKeyUp(KeyCode.UpArrow)) First_Arrow = 0;
        if (First_Arrow == 4 && Input.GetKeyUp(KeyCode.DownArrow)) First_Arrow = 0;
    }
}
