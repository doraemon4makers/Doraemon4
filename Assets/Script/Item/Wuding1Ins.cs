using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wuding1Ins : Item
{

    protected override void Start()
    {
        base.Start();

        //SetEnglishName("wuding1");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("wujia1", "tower");


    }

    public override void Use(Transform target)
    {
        transform.parent = target;

        //ZuHu temp = null;



    }
}
