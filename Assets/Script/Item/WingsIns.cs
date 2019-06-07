using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsIns : Fly
{
    //public float movePower = 5;

    protected override void Start()
    {
        base.Start();

        //SetEnglishName("Wings");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        //GetComponent<ZuHu>().zuheDic.Add("wings_1", "wings_2");
    }

    public override void Use(Transform target)
    {
        transform.parent = target;
        transform.position = target.Find("BackPoint").position;
        target.GetComponent<Animator>().SetBool("shoot", true);
    }
    public  void Remove(Transform target)
    {
        transform.position = target.Find("DetachPoint").position;
        transform.parent = null;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = (transform.right + transform.up) * 20;
        GetComponent<Rigidbody2D>().angularVelocity = 180f;
    }

    protected void Flying()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        body.AddForce(direction * movePower * 0.5f);
    }

}
