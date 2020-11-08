using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public GameOver gameOver;
    public GameObject signObject;
    public int[] groundLevel; // 1..2..3
    private float[] percent; //시세할인가
    private bool firstStart = true; //최초로 시작하는건가?(맞다면 true)
    private MapDataScriptableObject mdso = null;

    public GameObject ajussiObj;
    public GameObject halmoneyObj;
    public GameObject truckObj;
    public GameObject cgObj;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        groundLevel = new int[5];
        percent = new float[4];
    }

    /// <summary>
    /// 마을회관에서 나와 게임을 시작할때 호출되는 함수
    /// </summary>
    public void GameStart()
    {

        if (firstStart && mdso == null)
        {
            //원래는 랜덤
            mdso = MapData.Instance.GetMapData(0);
            firstStart = false;
        }

        GameObject obj = Instantiate(ajussiObj);
        obj.transform.position = mdso.ajussiPosition;

        obj = Instantiate(halmoneyObj);
        obj.transform.position = mdso.halmoneyPosition;

        obj = Instantiate(truckObj);
        obj.transform.position = mdso.truckPosition;

        for(int i=0; i<mdso.CGPosition.Length; i++)
        {
            obj = Instantiate(cgObj);
            obj.transform.position = mdso.CGPosition[i];
            CultivatedGround cg = obj.GetComponent<CultivatedGround>();
            cg.GenerateTile(groundLevel[i]);
        }
    }

    /// <summary>
    /// 시세변화율을 업데이트하는 함수
    /// </summary>
    private void RenewSaleprice()
    {
        float minPercent = -15.0f;
        float maxPercent = 15.0f;
        
        for(int i=0; i<percent.Length; i++)
        {
            float value = Random.Range(minPercent, maxPercent);
            int integer = (int)value;
            percent[i] = (float)integer;
        }
    }
}
