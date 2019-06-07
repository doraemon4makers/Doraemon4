using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : GroupableItem
{
    public override SlotType slotType
    {
        get
        {
            return SlotType.Back;
        }
    }
    public bool canFly { get { return testFly || owner != null; } }
    public bool testFly;

    public float movePower = 10;

    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody2D>();

        attachPointName = "BackPoint";
    }

    public override void Group(IGroupable other)
    {
        UnGroup();
        base.Group(other);

        (owner as Player).isFly = true;

        body.isKinematic = true;
        //对象的标签为 player
        gameObject.tag = "Fly";
        owner.ShouldShowUI = true;

        //this.owner = owner;
        //this.owner.isFly = true;
        //GameController.Attach(transform, owner.transform.Find("BackPoint"));
        ////物体自身的是否为动力学刚体显示为 true

    }
    public override void UnGroup()
    {
        if (owner == null) return;

        (owner as Player).isFly = false;

        body.velocity = Vector3.zero;
        //物体动力学刚体为 true
        body.isKinematic = true;
        //此时对象的标签为 ground
        gameObject.tag = "Fly";

        base.UnGroup();

        ////this 的用法是怎么用的？
        //if (this.owner)
        //{
        //    //gamecontroller的用法是如何，它有什么样的功能
        //    GameController.Detach(this.owner.transform, transform);
        //    //this 主要是用来区分 变量名   局部变量 与类的成员变量 ——有效范围最大的是成员变量  其次方法的参数变量 
        //    //再其次是 if或循环体里的变量  作用域主要就是根据大括号来划分  小括号里的参数由外部来传
        //    this.owner.isFly = false;
        //    owner.ShouldShowUI = false;
        //    //？？
        //    this.owner = null;
        //}
        //body.velocity = Vector3.zero;
        ////物体动力学刚体为 true
        //body.isKinematic = true;
        ////此时对象的标签为 ground
        //gameObject.tag = "Fly";

    }
}
