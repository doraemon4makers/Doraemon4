using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGroupable
{
    Transform trans { get; }
    SlotType slotType { get; }
    bool HasGroup { get; set; }
    bool ShouldShowUI { get; set; }
    bool IsOwner { get; set; }
    bool IsGroupParent { get; set; }
    void Group(IGroupable other);
    void UnGroup();
}