using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private int Minute = 0; //분-저장
    public int Hour = 9; //시간-저장 // 최초시간은 아침9시
    public float clock_speed; //게임속 1시간이 몇초인가?
    private float tic_toc;
    [HideInInspector]
    public float currentTime; //축적된 시간을 저장 - SPKoon
    public bool Day = true; //시간이 흘러가도 괜찮은가?
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("clock_N"); //clock.cs 와 clock_N 을 연결.
    }

    // Update is called once per frame
    void Update()
    {
        if (Day == true)
        {
            tic_toc += Time.deltaTime;
            currentTime += Time.deltaTime; // -SPKoon
            if (clock_speed < tic_toc)
            {
                Minute += 30;
                if (Minute == 60)
                {
                    Minute = 0;
                    Hour += 1;
                    if (Hour == 18)
                    {
                        Day = false; //시간정지.
                        GameManager.Instance.GameOver();
                    }
                }
                tic_toc = 0;
            }
        }
        GetComponent<Text>().text = Hour + " : " + Minute / 10 + "0";
    }

    public int GetHour()
    {
        return Hour;
    }
}
