using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColdWeapon coldWeapon = transform.parent.GetComponentInChildren<ColdWeapon>();
        if (coldWeapon != null)
        {
            EventManager.ExecuteEvent("ColdWeaponHurt", collision);
        }
    }
}
