using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class testScript : MonoBehaviour
{
    public PixelPerfectCamera cam;

    public void OnClick()
    {
        cam.enabled = !cam.enabled;
    }



}
