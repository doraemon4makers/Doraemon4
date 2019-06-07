using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { None, Back, Head, Hand, LeftHand, RightHand, VehicleSeat }

public class SlotMarker : MonoBehaviour
{
    public SlotType slotType;
    public bool IsOccupied { get { return GetComponentInChildren<IGroupable>() != null; } }
}
