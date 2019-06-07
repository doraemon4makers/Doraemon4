using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liang1Ins : Item

{
    protected override void Start()
    {
        base.Start();

        //SetEnglishName("liang1");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("zhuzi2", "wujia2");

        GetComponent<ZuHu>().zuheDic.Add("zhuzi", "wuding1");
    }

    public override void Use(Transform target)
    {
        transform.parent = target;

        //ZuHu temp = null;



    }



}
