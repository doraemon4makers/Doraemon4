using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour

{
    //存储长度
    public float length;
    //存储玩家位置
    private Transform player;
    //存储目标向量
    private Vector3 target;
    //判断是否移动，true-在移动，false-不移动
    private bool isMove;
    //起始方法
    private void Start()
    {
        //玩家变量 找到玩家的位置
        player = GameObject.Find("Player1").transform;
    }
    private void LateUpdate()//摄像机跟随通常在LateUpdate中
    {
        //如果游戏管理的实例化游戏声明 为 游戏中
        if (GameManager.ins.GameState == GameState.InGame)
            //则执行跟随方法
            Follow();
       
    }

    void Follow()
    {
        //超出左边界
        //假如（玩家x轴位置小于等于摄像机自身x轴位置减去length的值
        if (player.position.x <= (transform.position.x - length))
        {
            //则目标为自身位置 减去 length函数*右方向
            target = transform.position - length * Vector3.right;
            //此时 是否移动 为 在移动
            isMove = true;

        }
        //超出右边界
        //假如（玩家x轴位置大于等于摄像机自身x轴位置加上length的值
        else if (player.position.x >= (transform.position.x + length))
        {
            //则执行目标为自身位置 加上 length函数*右方向
            target = transform.position + length * Vector3.right;
            //此时 是否移动 为 在移动
            isMove = true;
        }
        //假如（玩家y轴位置大于等于摄像机自身y轴位置减去length的值
        if (player.position.y <= (transform.position.y - length))
        {
            //则执行目标为自身位置 减去 length函数*向上方向
            target = transform.position - length * Vector3.up;
            isMove = true;

        }
        //超出上边界
        //假如（玩家y轴位置大于等于摄像机自身y轴位置加上length的值
        else if (player.position.y >= (transform.position.y + length))
        {
            //则执行目标为自身位置 加上 length函数*向上方向
            target = transform.position + length * Vector3.up;
            isMove = true;

        }
        else
            isMove = false;
        //如果移动
        if (isMove)
        {
            // 3*Time.deltaTime*target  + （1-3*Time.deltaTime）*transform.position
            //则执行自身位置为 lerp方向（参数为 自身位置，目标，以每秒钟3米的速度移动
            transform.position = Vector3.Lerp(transform.position, target, 3 * Time.deltaTime);
        }
    }



}
