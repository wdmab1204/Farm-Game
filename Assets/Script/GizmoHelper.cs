using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoHelper : MonoBehaviour
{
    public enum Mode
    {
        scale,
        collider,
        custom
    }
    public Mode mode;
    public float x = 1f;
    public float y = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        switch (mode)
        {
            case Mode.scale:
                Gizmos.DrawWireCube(transform.position, transform.localScale);
                break;
            case Mode.collider:
                Vector2 size = GetComponent<BoxCollider2D>().size;
                Gizmos.DrawWireCube(transform.position, size);
                break;
            case Mode.custom:
                Gizmos.DrawWireCube(transform.position, new Vector2(x, y));
                break;
        }
        
    }
}
