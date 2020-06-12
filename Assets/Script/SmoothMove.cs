using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public Vector3 destination;
    public float speed = 0.1f;

    void Start()
    {
        float x, y;
        x = Random.Range(-0.7f, 0.7f);
        y = Random.Range(-0.7f, 0.7f);
        destination = new Vector3(transform.position.x + x, transform.position.y + y, 0f);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
    }
}
