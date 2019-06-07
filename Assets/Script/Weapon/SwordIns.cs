using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIns :ColdWeapon
{
    public override SlotType slotType
    {
        get
        {
            return SlotType.RightHand;
        }
    }

    protected override void Start()
    {
        base.Start();

        //SetEnglishName("sword");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

        //GetComponent<ZuHu>().zuheDic.Add("wings_1", "wings_2");
        EventManager.RegisterEvent<Collider2D>("ColdWeaponHurt", OnColdWeaponHurt);
    }

    public override void Use(Transform target)
    {
        SlotMarker[] slots = target.GetComponentsInChildren<SlotMarker>();

        foreach(SlotMarker slot in slots)
        {
            if(slot.slotType == this.slotType)
            {
                transform.parent = slot.transform;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }
    }
    private void OnColdWeaponHurt(Collider2D collision)
    {
        IDamagable hurter = collision.GetComponent<IDamagable>();
        if(hurter!= null && !hurter.isPlayerSide)
        {
            hurter.BeDamage(1);
        }
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

    
}
