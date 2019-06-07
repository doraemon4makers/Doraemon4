using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zhuzi2Ins: Item

{
    protected override void Start()
    {
        base.Start();

        //SetEnglishName("zhuzi2");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("liang", "wujia");
    }

    public override void Use(Transform target)
    {
        transform.parent = target;

        //ZuHu temp = null;



    }




}
