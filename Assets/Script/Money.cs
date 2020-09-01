using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

internal struct Constants
{
    public const int MIN_MONEY = 0;
    public const int MAX_MONEY = 9999999;
}

public class Money : MonoBehaviour
{
    private Text text;
    private int money;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetMoney(int money)
    {
        this.money = money;
        text.text = this.money.ToString();
    }

    public bool UseMoney(int money)
    {
        int result = this.money - money;
        if (result >= Constants.MIN_MONEY)
        {
            this.money = result;
            text.text = this.money.ToString();
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SaveMoney(int money, float dotweenDelay = 0)
    {
        this.money += money;
        text.DOText(money.ToString(), 3.0f, true, ScrambleMode.Numerals).SetDelay(dotweenDelay).SetUpdate(true);
        //text.text = this.money.ToString();
    }

    public int GetMoney()
    {
        return this.money;
    }



}
