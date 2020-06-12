using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoHelper : MonoBehaviour
{
    public float x = 1f;
    public float y = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(x, y));
    }
}
