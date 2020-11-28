using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{
    public int number;
    private void Start()
    {
        Debug.Log("Position : " + transform.position);
        Debug.Log("LocalPosition : " + transform.localPosition);
    }

}
