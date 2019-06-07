using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSystem1

{
    /// <summary>
    /// 存储接取的任务 《任务ID ， 任务信息》
    /// </summary>

    private Dictionary<string, MissionArg1> missionsDic = new Dictionary<string, MissionArg1>();
    //instance 是实例的意思
    private static MissionSystem1 ins;

    public delegate void MissionListChangeEventHandle(MissionArg1[] missionArgs);

    public static event MissionListChangeEventHandle MissionChangeEvent;

    


    private MissionSystem1()
    { }

    /// <summary>
    /// 接取任务
    /// </summary>
    /// <param name="missionID">任务ID</param>
    public static void ReceiveMission(MissionArg1 mission)
    {
        //第一次接取任务的时候 生成实例
        if (ins == null)
        {
            ins = new MissionSystem1();
        }

        //判断是否有该任务 
        if (!ins.missionsDic.ContainsKey(mission.MissionID))
        {
            //没有
            //中间值
           // MissionArg1 mission = new MissionArg1(missionID);

            //加入到任务列表
            ins.missionsDic.Add(mission.MissionID, mission);

            Debug.Log("注册成功 ！ 任务 ID ：" + mission.MissionID + " 现在任务数量 ： " + ins.missionsDic.Count);



           
        }
        else
        {
            Debug.Log("注册失败 ！ 已注册的任务 ID ：" + mission.MissionID);
        }
    }

    /// <summary>
    /// 注销任务
    /// </summary>
    /// <param name="missionID">任务ID</param>
    public static void CancelMission(string missionID)
    {
        if (ins == null)
        {
            Debug.Log("没有任何任务 ！");
            return;
        }

        //判断是否有该任务 
        if (ins.missionsDic.ContainsKey(missionID))
        {
            //有
            //注销 
            ins.missionsDic.Remove(missionID);
            Debug.Log("注销成功 ！ 任务 ID ：" + missionID);


            if (ins.missionsDic.Count == 0)
            {
                //通关

            }

           
        }
        else
        {
            Debug.Log("注销失败 ！ 没有该任务 ID ：" + missionID);
        }
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    /// <param name="missionID">任务ID</param>
    public static void UpdateMission(string missionID)
    {

        if (ins == null)
        {
            Debug.Log("没有任何任务 ！");
            return;
        }

        //中间值
        MissionArg1 mission = null;

        //检查是否有该任务
        if (ins.missionsDic.TryGetValue(missionID, out mission))
        {
            //有
            //提交 
            //mission.CompleteMission();

            mission.UpdateMission();
            Debug.Log("提交成功 ！ 任务 ID ：" + missionID);

        
        }
        else
        {
            Debug.Log("提交失败 ！ 没有该任务 ID ：" + missionID);
        }
    }

   

    /// <summary>
    /// 获取任务列表
    /// </summary>
    /// <returns>任务列表</returns>
    MissionArg1[] GetCurrentMissions()
    {
        //是否有接取过任务
        if (ins == null)
        {
            return null;
        }

        //是否有正在执行的任务
        if (ins.missionsDic.Count == 0)
        {
            return null;
        }

        //创建中间变量
        MissionArg1[] targetArrary = new MissionArg1[ins.missionsDic.Count];

        int temp = 0;

        //遍历 将所有字典中的值 存入中间变量中
        foreach (MissionArg1 mission in ins.missionsDic.Values)
        {
            targetArrary[temp] = mission;

            temp++;
            //Debug.Log(mission.MissionID);
        }

        //foreach (MissionArg mission in targetArrary)
        //{
        //    Debug.Log(mission.MissionID);
        //}

        return targetArrary;
    }


}
