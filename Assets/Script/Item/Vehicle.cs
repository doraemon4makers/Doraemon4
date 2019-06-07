using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : GroupableItem
{
    public override SlotType slotType
    {
        get
        {
            return SlotType.None;
        }
    }


    public bool canDrive { get { return testDrive || (owner != null); } }
    public bool testDrive;

    public float movePower = 10;   


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        IsGroupParent = true;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BeDamage(1);
        }

        if(canDrive)
        {
            Drive();
        }
    }

    protected virtual void Drive()
    {

    }

    public override void Group(IGroupable other)
    {
        UnGroup();

        base.Group(other);

        (owner as Player).isDriving = true;

        //物体自身的是否为动力学刚体显示为 false
        body.isKinematic = false;
        //对象的标签为 player
        gameObject.tag = "Player";
    }

    public override void UnGroup()
    {
        if (owner == null) return;

        body.velocity = Vector3.zero;
        //物体动力学刚体为 true
        body.isKinematic = true;
        //此时对象的标签为 ground
        gameObject.tag = "Ground";

        (owner as Player).isDriving = false;

        base.UnGroup();


        ////this 的用法是怎么用的？
        //if (this.driver)
        //{
        //    //gamecontroller的用法是如何，它有什么样的功能
        //    GameController.Detach(this.driver.transform, transform);
        //    //this 主要是用来区分 变量名   局部变量 与类的成员变量 ——有效范围最大的是成员变量  其次方法的参数变量 
        //    //再其次是 if或循环体里的变量  作用域主要就是根据大括号来划分  小括号里的参数由外部来传
        //    this.driver.isDriving = false;
        //    //？？
        //    this.driver = null;
        //}
        ////否则 物体身体的力的向量为 原点
        //body.velocity = Vector3.zero;
        ////物体动力学刚体为 true
        //body.isKinematic = true;
        ////此时对象的标签为 ground
        //gameObject.tag = "Ground";
    }


    
}
