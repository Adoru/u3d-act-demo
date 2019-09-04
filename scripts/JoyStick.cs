using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    private bool isPress = false;
    private Transform button;
    public static float h;
    public static float v;

    private void Awake()
    {
        button = transform.Find("Button");
    }

    void OnPress(bool isPress)  //NGUI内置方法
    {
        this.isPress = isPress;
        if (isPress == false)
        {
            button.localPosition = Vector3.zero;
            h = 0;
            v = 0;
        }
    }

    private void Update()
    {
        if(isPress)
        {
            Vector2 touchPos = UICamera.lastEventPosition;
            touchPos -= new Vector2(100, 100);
            float distance = Vector2.Distance(Vector2.zero, touchPos);
            if(distance > 73)
            {
                touchPos = touchPos.normalized * 73;
                button.localPosition = touchPos;
            }
            else
            {
                button.localPosition = touchPos;     //二维向量赋值给三维向量z默认为0
            }

            //水平方向移动值
            h = touchPos.x / 73;
            v = touchPos.y / 73;
        }
    }
}
