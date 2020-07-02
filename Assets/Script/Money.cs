using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private Player player;
    private Text text;
    public int money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            text.text = value.ToString();
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        text = GetComponent<Text>();
    }

    public void SetMoney(int money)
    {
        this.money = money;
        text.text = this.money.ToString();
    }


    
}
