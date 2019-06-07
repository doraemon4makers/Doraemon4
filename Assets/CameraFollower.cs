using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private int currentIndex;

    public int maxIndex;

    public float moveDis;

    public float moveSpeed;

    private bool needMove;

    private Vector3 targetPos;


    public void Move(int dir)
    {
        if (dir < 0)
            MoveLeft();
        else
            MoveRight();
    }

    private void MoveRight()
    {
        if (currentIndex >= maxIndex)
            return;

        currentIndex++;

        needMove = true;

        targetPos = new Vector3(transform.position.x + moveDis, transform.position.y,transform.position.z);
    }

    private void MoveLeft()
    {
        if (currentIndex <= 0)
            return;

        currentIndex--;

        needMove = true;

        targetPos =new Vector3 (transform.position.x - moveDis, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (needMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                transform.position = targetPos;

                needMove = false;
            }
        }
    }
}
