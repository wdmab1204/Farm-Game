using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{

    public LayerMask layerMask;
    public float distance;


    private void Start()
    {
       
    }
    private void FixedUpdate()
    {
        Test();
    }

    private void Test()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position, transform.localScale, 0, layerMask);
        //DebugDrawBox(transform.position, transform.localScale, 0, Color.red, 0.1f);
        //Collider2D col = Physics2D.OverlapArea(
        //    new Vector2(transform.position.x - distance, transform.position.y - distance),
        //    new Vector2(transform.position.x + distance, transform.position.y + distance),
        //    layerMask);
        Debug.Log(col);
        
    }

    void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration)
    {

        var orientation = Quaternion.Euler(0, 0, angle);

        // Basis vectors, half the size in each direction from the center.
        Vector2 right = orientation * Vector2.right * size.x / 2f;
        Vector2 up = orientation * Vector2.up * size.y / 2f;

        // Four box corners.
        var topLeft = point + up - right;
        var topRight = point + up + right;
        var bottomRight = point - up + right;
        var bottomLeft = point - up - right;

        // Now we've reduced the problem to drawing lines.
        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomRight, bottomLeft, color, duration);
        Debug.DrawLine(bottomLeft, topLeft, color, duration);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("enter " + collision.gameObject.name);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("exit " + collision.gameObject.name);

    //}


}
