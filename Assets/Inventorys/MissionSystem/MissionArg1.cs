using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//???
using System;


//Arg任务参数的意思
public class MissionArg1

{

    public string MissionID { get { return missionID; } }

    public string MissionTitle { get { return missionTitle; } }

    public string MissionInner { get { return missionInner; } }

    public int TargetCount { get { return targetCount; } }

    public int CurrentCount { get { return currentCount; } }

    //任务ID
    private string missionID;
    //任务标题
    private string missionTitle;
    //任务内容
    private string missionInner;
    //任务目标数量
    private int targetCount;
    //任务目标现在的数量
    private int currentCount;

    public MissionArg1(string missionID)
    {
        this.missionID = missionID;

        //LoadInformation();
    }

    //private void LoadInformation()
    //{
    //    throw new NotImplementedException();
    //}


    /// <summary>
    /// 加载任务信息
    /// </summary>
    // void LoadInformation()
    // {
    //PlayerPrefs 保存轻量级的数据 存储读取比较频繁的数据 音量 背包 
    //XML 
    //Json
    //Csv 不需要专业技能也能看懂 数据关联性较差 不方便数据查找
    //序列化 、反序列化
    //ScriptableObject

    //missionTitle = "测试用任务标题";
    //missionInner = "测试用任务内容";
    //targetCount = 10;
    //Item[] items = new Item[] { Item.Ins("Equipment_Gun_1", 1) };
    //reward = new RewardArg(items,100,100);

    //TextAsset text = Resources.Load<TextAsset>("MissionTest");//相对路径 相对于Resources文件夹的路径

    //Debug.Log(text.text);

    ////分行
    //string[] lines = text.text.Split('\r');

    //for (int i = 0; i < lines.Length; i++)
    //{
    //    Debug.Log(lines[i]);
    //}

    ////获得目标任务数据所在行
    //string targetMission = lines[int.Parse(missionID)];

    //string[] datas = targetMission.Split(',');

    ////数据匹配
    //missionTitle = datas[0];
    //missionInner = datas[1];
    //int num = 0;

    //if (int.TryParse(datas[2], out num))
    //{
    //    targetCount = num;
    //}

    //missionTitle = missionData[int.Parse(missionID)][0];

    //missionInner = missionData[int.Parse(missionID)][1];

    ////targetCount = int.Parse(missionData[int.Parse(missionID)][2]);
    //int num = 0;

    //if (int.TryParse(missionData[int.Parse(missionID)][2], out num))
    //{
    //    targetCount = num;
    //}
    // }

    public void SetValue(string missionTitle, string missionInner, string targetCount)
    {
        this.missionTitle = missionTitle;
        this.missionInner = missionInner;
        this.targetCount = int.Parse(targetCount);
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    private void CompleteMission()
    {
        MissionSystem1.CancelMission(missionID);
        Debug.Log("任务完成");
    }

    /// <summary>
    /// 设置任务目标情况 
    /// </summary>
    /// <param name="currentValue"></param>
    public void UpdateMission()
    {

        //更改数量
        currentCount++;

        //如果数量足够 
        if (currentCount == targetCount)
        {
            //完成任务的操作

            CompleteMission();

            
        }

    }

}
