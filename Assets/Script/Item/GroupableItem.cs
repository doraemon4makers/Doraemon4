using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupableItem : Item, IGroupable
{
    public string attachPointName = "AttachPoint";
    public IGroupable owner { get; private set; }
    public bool IsGroupParent { get; set; }

    public bool ShouldShowUI
    {
        get;
        set;
    }

    public bool IsOwner { get; set; }

    public Transform trans
    {
        get
        {
            return transform;
        }
    }

    public abstract SlotType slotType
    {
        get;
    }

    public bool HasGroup
    {
        get;

        set;
    }

    public virtual void Group(IGroupable other)
    {
        UnGroup();

        GameController.TryAttach(trans, other.trans);

        //if(IsGroupParent)
        //{
        //    GameController.Attach(other.trans, trans, attachPointName);
        //}
        //else
        //{
        //    GameController.Attach(trans, other.trans, attachPointName);
        //}

        owner = other;
        HasGroup = true;
    }

    public virtual void UnGroup()
    {
        if (owner == null) return;

        if (IsGroupParent)
        {
            GameController.Detach(owner.trans, trans);
        }
        else
        {
            GameController.Detach(trans, owner.trans);
        }

        owner.ShouldShowUI = false;
        owner = null;
        HasGroup = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ShouldShowUI = true;
        IsOwner = false;
    }
}
