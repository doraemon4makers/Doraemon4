using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhuziIns : Item

{

    protected override void Start()
    {
        base.Start();

        //SetEnglishName("zhuzi");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("liang1", "wuding1");

       
    }

    public override void Use(Transform target)
    {
        transform.parent = target;

        //ZuHu temp = null;



    }
}
