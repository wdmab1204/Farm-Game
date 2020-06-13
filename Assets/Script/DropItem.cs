using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    private Collider2D col;
    public Item item;
    private void Start()
    {
        Invoke("OnOffCollider", 1.5f);
    }

    void OnOffCollider()
    {
        col.enabled = !col.enabled;
    }
}
