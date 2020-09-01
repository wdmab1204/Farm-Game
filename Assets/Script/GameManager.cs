using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    private Image timeImage;
    private Image upImage;
    private Canvas canvas;
    public GameObject timeUpImageObject;
    public GameObject resultImageObject;
    private Truck truck;
    private Sequence timeUpSeq;
    private Transform[] resultImageChildArray;

    public Ease timeUpEnterEase;
    public Ease timeUpExitEase;
    public Ease resultEnterEase;
    public Ease resultExitEase;

    public ScrambleMode moneyScrambleMode;


    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        truck = GameObject.FindGameObjectWithTag("Truck").GetComponent<Truck>();

        GameObject obj = GameObject.Find("Time Up Image");
        if (obj == null) obj = Instantiate(timeUpImageObject, canvas.transform);

        timeImage = obj.transform.GetChild(0).GetComponent<Image>();
        upImage = obj.transform.GetChild(1).GetComponent<Image>();


        obj = GameObject.Find("Result Images");
        if (obj == null) obj = Instantiate(resultImageObject, canvas.transform);
        RectTransform resultImageRC = resultImageObject.GetComponent<RectTransform>();
        resultImageRC.position = new Vector2(Screen.width, 0f);
        resultImageChildArray = new Transform[obj.transform.childCount];
        for (int i = 0; i < resultImageChildArray.Length; i++) resultImageChildArray[i] = obj.transform.GetChild(i);


        timeUpSeq = DOTween.Sequence();
        timeUpSeq.Append(timeImage.rectTransform.DOAnchorPosY(Screen.height, 2).From().SetEase(timeUpEnterEase).SetUpdate(true));
        timeUpSeq.Join(upImage.rectTransform.DOAnchorPosY(Screen.height, 2.5f).From().SetEase(timeUpEnterEase).SetUpdate(true)
            .OnComplete(() =>
            {
                List<Item> list = truck.list;
                int[] gold = new int[5]; // 0 : nothing, 1 : Cereal, 2 : Bulbous, 3 : Fruit, 4 : Vegetable
                for (int i = 0; i < list.Count; i++)
                {
                    gold[(int)list[i].cropType] += list[i].purchasePrice;
                }


                timeImage.rectTransform.DOAnchorPosX(-Screen.width, 1.5f).SetEase(timeUpExitEase).SetUpdate(true);
                upImage.rectTransform.DOAnchorPosX
                    (-Screen.width + (upImage.rectTransform.position.x - timeImage.rectTransform.position.x), 1.5f).SetEase(timeUpExitEase).SetUpdate(true).
                    OnComplete(() =>
                    {
                        RectTransform rt;
                        Text text;
                        int resultMoney = 0;
                        for (int i = 0; i < resultImageChildArray.Length; i++)
                        {
                            text = resultImageChildArray[i].GetChild(1).GetComponent<Text>();
                            int money = gold[i + 1];//(gold[i + 1] >= maxMoney) ? gold[i + 1] : maxMoney;
                            resultMoney += money;


                            rt = resultImageChildArray[i].GetComponent<RectTransform>();
                            rt.DOAnchorPosX(-Screen.width, 1.5f).SetEase(resultEnterEase).SetUpdate(true).SetDelay(i * 0.25f);

                            text.DOText("Hello World", 1.5f, true, moneyScrambleMode).SetDelay(i * 0.25f + 0.25f).SetUpdate(true);
                        }
                        Player.Instance.money.SaveMoney(5000, 1.5f);
                    });


            }));
        timeUpSeq.SetUpdate(true);
        timeUpSeq.Pause();

    }

    public void TimeUp()
    {
        Time.timeScale = 0f;
        timeUpSeq.Play();

    }

    public void Complete()
    {
        timeUpSeq.Complete();
    }

}
