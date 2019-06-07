using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private void Awake()
    {
        MissionArg1 mission = new MissionArg1("M1");

        mission.SetValue("HIAT1", "生成一些物体，使得你可以绕过敌人到达出口处，并在出口处生成一些古建筑的基本元素", "3");

        MissionSystem1.ReceiveMission(mission);

        //uim 调用UIManager 中 更改mission的方法
        UIManager.ins.AddMisionUI(mission.MissionTitle, mission.MissionInner);

        MissionArg1 mission1 = new MissionArg1("M2");

        mission.SetValue("HIAT2", "用生成的古建筑基本元素，进行组合，组合好古建筑的完成形态后，你将会获得一个道具", "3");

        MissionSystem1.ReceiveMission(mission);

        //uim 调用UIManager 中 更改mission的方法
        UIManager.ins.AddMisionUI(mission.MissionTitle, mission.MissionInner);

        MissionArg1 mission2 = new MissionArg1("M3");

        mission.SetValue("HIAT3", "用新获得的道具消灭敌人，获得敌人身上的宝物", "3");

        MissionSystem1.ReceiveMission(mission);

        //uim 调用UIManager 中 更改mission的方法
        UIManager.ins.AddMisionUI(mission.MissionTitle, mission.MissionInner);



    }

}
