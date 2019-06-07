using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColdWeapon : GroupableItem
{
    public bool canFly { get { return testFly || owner != null; } }
    public bool testFly;

    public float movePower = 10;

    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody2D>();

        attachPointName = "rfist_CTRL";
    }

    public override void Group(IGroupable other)
    {
        UnGroup();
        base.Group(other);

        body.isKinematic = true;
        //对象的标签为 player
        gameObject.tag = "ColdWeapon";
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
        gameObject.tag = "ColdWeapon";

        base.UnGroup();
    }

    }
