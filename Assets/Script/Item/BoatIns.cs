using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatIns : Vehicle
{
    protected override void Start()
    {
        base.Start();

        //SetEnglishName("boat");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("gun", "robot");
    }

    public override void Use(Transform target)
    {
        transform.parent = target;

        //ZuHu temp = null;
        GameObject player = GameObject.Find("Player1");
        player.GetComponent<Animator>().SetBool("shoot", true);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Drive()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        body.AddForce(direction * movePower*0.5f);
    }
}
