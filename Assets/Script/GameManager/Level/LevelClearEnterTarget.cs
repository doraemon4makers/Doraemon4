using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearEnterTarget : MonoBehaviour
{
    /// <summary>
    /// 可触发的游戏对象名称
    /// </summary>
    public string[] triggerNames;
    //public string[] triggerEnglishNames;
    /// <summary>
    /// 记录碰撞状态
    /// </summary>
    private bool[] isEnters;

    private void Awake()
    {
        //Debug.Log("Awake InitIsEnter");
        InitIsEnters();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enter");
        InitIsEnters();
        //Debug.Log("OnTriggerEnter2D InitIsEnter");
        if (isEnters == null) return;

        //Debug.Log("比较名字");
        string collisionName = collision.name;
        for (int i = 0; i < triggerNames.Length; i++)
        {
            if (collisionName.Equals(triggerNames[i]))
            {
                isEnters[i] = true;
            }
        }
        //Debug.Log("ExecuteEvent");
        ExecuteEvent();

        //IHasID hasID = collision.GetComponent<IHasID>();
        //if(hasID != null)
        //{
        //    string collisionEnglishName = hasID.englishName;
        //    for (int i = 0; i < triggerEnglishNames.Length; i++)
        //    {
        //        if (collisionEnglishName.Equals(triggerEnglishNames[i]))
        //        {
        //            isEnters[i] = true;
        //        }
        //    }
        //    //Debug.Log("ExecuteEvent");
        //    ExecuteEvent();
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InitIsEnters();
        if (isEnters == null) return;

        string collisionName = collision.name;
        for (int i = 0; i < triggerNames.Length; i++)
        {
            if (collisionName.Equals(triggerNames[i]))
            {
                isEnters[i] = false;
            }
        }
        ExecuteEvent();

        //IHasID hasID = collision.GetComponent<IHasID>();
        //if(hasID != null)
        //{
        //    string collisionName = hasID.englishName;
        //    for (int i = 0; i < triggerEnglishNames.Length; i++)
        //    {
        //        if (collisionName.Equals(triggerEnglishNames[i]))
        //        {
        //            isEnters[i] = false;
        //        }
        //    }
        //    ExecuteEvent();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Enter");
        InitIsEnters();
        //Debug.Log("OnTriggerEnter2D InitIsEnter");
        if (isEnters == null) return;

        //Debug.Log("比较名字");
        string collisionName = collision.gameObject.name;
        for (int i = 0; i < triggerNames.Length; i++)
        {
            if (collisionName.Equals(triggerNames[i]))
            {
                isEnters[i] = true;
            }
        }
        //Debug.Log("ExecuteEvent");
        ExecuteEvent();

        //IHasID hasID = collision.gameObject.GetComponent<IHasID>();
        //if(hasID != null)
        //{
        //    string collisionEnglishName = hasID.englishName;
        //    for (int i = 0; i < triggerEnglishNames.Length; i++)
        //    {
        //        if (collisionEnglishName.Equals(triggerEnglishNames[i]))
        //        {
        //            isEnters[i] = true;
        //        }
        //    }
        //    //Debug.Log("ExecuteEvent");
        //    ExecuteEvent();
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        InitIsEnters();
        if (isEnters == null) return;

        string collisionName = collision.gameObject.name;
        for (int i = 0; i < triggerNames.Length; i++)
        {
            if (collisionName.Equals(triggerNames[i]))
            {
                isEnters[i] = false;
            }
        }
        ExecuteEvent();

        //IHasID hasID = collision.gameObject.GetComponent<IHasID>();
        //if(hasID != null)
        //{
        //    string collisionEnglishName = hasID.englishName;
        //    for (int i = 0; i < triggerEnglishNames.Length; i++)
        //    {
        //        if (collisionEnglishName.Equals(triggerEnglishNames[i]))
        //        {
        //            isEnters[i] = false;
        //        }
        //    }
        //    ExecuteEvent();
        //}
    }

    /// <summary>
    /// 初始化记录碰撞状态数组
    /// </summary>
    private void InitIsEnters()
    {
        if(isEnters == null && triggerNames != null)
        {
            isEnters = new bool[triggerNames.Length];
            for(int i = 0; i < isEnters.Length; i++)
            {
                isEnters[i] = false;
            }
        }
        //if (isEnters == null && triggerEnglishNames != null)
        //{
        //    isEnters = new bool[triggerEnglishNames.Length];
        //    for (int i = 0; i < isEnters.Length; i++)
        //    {
        //        isEnters[i] = false;
        //    }
        //}
    }

    /// <summary>
    /// 根据碰撞状态数组发送事件
    /// </summary>
    private void ExecuteEvent()
    {
        bool hasEnter = false;
        foreach(bool temp in isEnters)
        {
            hasEnter = hasEnter || temp;
            if (hasEnter) break;
        }

        if (hasEnter)
        {
            //Debug.Log("hasEnter");
            EventManager.ExecuteEvent("LevelClearTargetEntered");
        }
        else
        {
            //Debug.Log("!hasEnter");
            EventManager.ExecuteEvent("LevelClearTargetExited");
        }
    }
}
