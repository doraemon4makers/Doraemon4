using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IGroupable
{
    public bool isDriving;
    public bool isFly;

    public Transform trans { get { return transform; } }

    public SlotType slotType { get { return SlotType.VehicleSeat; } }

    public bool ShouldShowUI { get; set; }
    public bool IsOwner { get; set; }
    public bool IsGroupParent { get; set; }

    public bool HasGroup
    {
        get;

        set;
    }

    List<IGroupable> groupables = new List<IGroupable>();

    public void Group(IGroupable other)
    {
        groupables.Add(other);
        HasGroup = true;
    }

    public void UnGroup()
    {
        foreach(IGroupable other in groupables)
        {
            if (other.IsGroupParent)
            {
                GameController.Detach(trans, other.trans);
            }
            else
            {
                GameController.Detach(other.trans, trans);
            }

            other.HasGroup = false;
        }

        groupables.Clear();
        HasGroup = false;
    }
}
