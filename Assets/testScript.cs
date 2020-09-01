using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{
    private Text text;
    public ScrambleMode sm;
    private void Start()
    {
        text = GetComponent<Text>();
        text.DOText("Hello World", 3.0f, true, sm);
    }

}
