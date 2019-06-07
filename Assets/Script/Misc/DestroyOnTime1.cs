using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime1 : MonoBehaviour

{
    public float time;

    void Start()
    {
        //Instantiate//生成
        Destroy(gameObject, time);//摧毁 目标对象 多久之后摧毁
    }



}
