using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGuideWeapon : MonoBehaviour
{
   protected float maxAngle = 30f;

    protected void SetDirection(Transform shotPos)
    {
        Vector3 direction = (MouseTarget() - shotPos.position).normalized;
        if(GetAngle(direction) > maxAngle)
        {
            direction = Quaternion.Euler(0, 0, maxAngle) * Vector3.right;
        }
        if(GetAngle(direction) < -maxAngle)
        {
            direction = Quaternion.Euler(0, 0, -maxAngle) * Vector3.right;
        }

        shotPos.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, direction));

    }

    public float GetAngle(Vector3 direction)
    {
        float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
        return angle;

    }
    public Vector3 MouseTarget()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v = Vector3.Scale(v, Vector3.right + Vector3.up);

        return v;
    }

}
