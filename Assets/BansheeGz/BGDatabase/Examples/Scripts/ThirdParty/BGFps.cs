/*
<copyright file="BGFps.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;

//original from http://wiki.unity3d.com/index.php?title=FramesPerSecond
namespace BansheeGz.BGDatabase.Example
{
    public partial class BGFps:MonoBehaviour
    {
        float deltaTime = 0.0f;
 
        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }
 
        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;
 
            var style = new GUIStyle();
 
            var rect = new Rect(0, 200, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 50;
            style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
            var msec = deltaTime * 1000.0f;
            var fps = 1.0f / deltaTime;
            var text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}