using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CultivatedGroundContract : Inventory
{
    public float interval;
    public GameObject uiObject;
    private RectTransform rt;
    private int historyIndex;
    public static bool UIOnOff;

    private bool canDo;
    private void Awake()
    {
        index = 0;
        historyIndex = 0;
        canDo = true;
        rt = uiObject.GetComponent<RectTransform>();
        UIOnOff = false;
        uiObject.SetActive(false);
    }

    public new void ScrollControl(float scroll, int max = 9)
    {
        if (canDo == false) return;
        base.ScrollControl(scroll, max);
    }

    protected override void SelectSlot()
    {
        if (historyIndex < index)
        {
            //rt.position += Vector3.left * interval;
            historyIndex = index;
            rt.DOAnchorPosX(rt.localPosition.x - interval, 0.5f);
        }
        else if (historyIndex > index)
        {
            //rt.position += Vector3.right * interval;
            historyIndex = index;
            rt.DOAnchorPosX(rt.localPosition.x + interval, 0.5f);
        }
        StartCoroutine(StartCoolDawn());
    }

    IEnumerator StartCoolDawn()
    {
        float i = 0;
        canDo = false;
        while (true)
        {
            i += Time.deltaTime;
            if (i >= 0.5f) break;
            yield return null;
        }
        canDo = true;
    }

    public void On()
    {
        UIOnOff = true;
        uiObject.SetActive(true);
    }

    public void Off()
    {
        UIOnOff = false;
        uiObject.SetActive(false);
    }
}
