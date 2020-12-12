using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CultivatedGroundContract : Inventory
{
    [Header("Animation")]
    public float interval;
    public GameObject uiObject;
    private RectTransform rt;
    private int historyIndex;
    public static bool UIOnOff;
    private bool canDo;

    [Header("Function")]
    public Button[] btns;
    public Text[] texts;
    public Sprite btnSprite;
    public Sprite btnPressSprite;
    private void Awake()
    {
        index = 0;
        historyIndex = 0;
        canDo = true;
        rt = uiObject.GetComponent<RectTransform>();
        UIOnOff = false;
        btns[0].onClick.AddListener(() => OnClickBuy(0));
        btns[1].onClick.AddListener(() => OnClickBuy(1));
        btns[2].onClick.AddListener(() => OnClickBuy(2));
        btns[3].onClick.AddListener(() => OnClickBuy(3));
        btns[4].onClick.AddListener(() => OnClickBuy(4));
        uiObject.SetActive(false);
    }

    private void Start()
    {
        
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

    public void OnClickBuy(int index)
    {
        Debug.Log("Index : " + index);
        //버튼 이미지 변경
        btns[index].GetComponent<Image>().sprite = btnSprite;

        //버튼 클릭했을때의 이미지 변경
        SpriteState spriteState = new SpriteState();
        spriteState = btns[index].spriteState;
        spriteState.pressedSprite = btnPressSprite;
        btns[index].spriteState = spriteState;

        //온클릭함수 변경
        btns[index].onClick.RemoveAllListeners();
        btns[index].onClick.AddListener(delegate { OnClickUpdate(index); });

        CGLevelUp(index);
    }

    public void OnClickUpdate(int index)
    {
        CGLevelUp(index);
    }

    private void CGLevelUp(int index)
    {
        if (GameData.groundLevel[index] + 1 <= 3)
        {
            GameData.groundLevel[index] += 1;
            texts[index].text = "Level : " + GameData.groundLevel[index].ToString();
        }
    }
}
