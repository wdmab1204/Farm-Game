using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;
    [SerializeField]
    private Transform pivot;

    private float timer;
    private float timerMax = .1f;
    private Renderer myRenderer;

    void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        if(pivot == null)
        {
            pivot = this.transform;
        }
    }

    void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(sortingOrderBase - pivot.position.y + 0.5 - offset);
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
