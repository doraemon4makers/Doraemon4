using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wujia2Ins : Item

{
    protected override void Start()
    {
        base.Start();

        //SetEnglishName("wujia2");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("wuding1", "tower");

       
    }

    public override void Use(Transform target)
    {
        transform.parent = target;

        //ZuHu temp = null;



    }

}
