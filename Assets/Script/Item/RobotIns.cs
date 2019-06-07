using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotIns : Vehicle
{
    protected override void Start()
    {
        base.Start();

        //SetEnglishName("robot");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");
       // MissionSystem1.UpdateMission("M2");
        // zuheDic.Add("boat", "robot");
    }

    public override void Use(Transform target)
    {
        transform.parent = target;
        
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Drive()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0, 0).normalized;

        body.AddForce(direction * movePower);
    }
}
