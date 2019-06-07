using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_WingsIns : GroupableItem
{
    public override SlotType slotType
    {
        get
        {
            return SlotType.Back;
        }
    }

    public float movePower = 5;

    protected override void Start()
    {
        base.Start();

        //SetEnglishName("Wings");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        GetComponent<ZuHu>().zuheDic.Add("wings_1", "wings_");

        attachPointName = "BackPoint";
    }

    public override void Use(Transform target)
    {
        transform.parent = target;
        transform.position = target.Find("AttachPoint").position;
        target.GetComponent<Animator>().SetBool("shoot", true);
    }

    public override void UnGroup()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = (transform.right + transform.up) * 20;
        GetComponent<Rigidbody2D>().angularVelocity = 180f;

        base.UnGroup();
    }

    public void Remove(Transform target)
    {
        transform.position = target.Find("DetachPoint").position;
        transform.parent = null;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = (transform.right + transform.up) * 20;
        GetComponent<Rigidbody2D>().angularVelocity = 180f;
    }

    protected void Fly()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        body.AddForce(direction * movePower * 0.5f);
    }

}
