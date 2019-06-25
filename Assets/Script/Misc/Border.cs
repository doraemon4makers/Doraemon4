using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public int dir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.GetComponentInChildren<Play1>())
        if (collision.GetComponentInChildren<Player>())
        {
            transform.GetComponentInParent<CameraFollower>().Move(dir);
        }

        if (collision.name == "ShiZiJun")
        {
            transform.GetComponentInParent<CameraFollower>().Move(dir);
        }
    }
}
