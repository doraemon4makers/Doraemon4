using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon1
{
    protected bool isPlayerWeapon;

    //存放射击点
    protected Transform shotPos;
    //存放子弹预设体
    protected GameObject bulletPrefab;
    //存放枪预设体
    protected GameObject gunPrefab;
    //存放最后射击时间
    protected float lastFireTime;
    //存放现在持有子弹
    public int currentBullet;
    //存放武器图标
    protected Sprite weaponIcon;
    //飞行速度只读属性 抽象方法的运用？？
    public abstract float flySpeed { get; }
    //枪预设体体只读属性
    protected abstract GameObject GunPrefab { get; }
    //最大子弹数量只读属性
    public abstract int MaxBulletCount { get; }
    //武器伤害属性
    protected abstract int WeaponDamage { get; }
    //抽象 存放子弹预设体属性
    protected abstract GameObject BulletPrefab { get; }
    // 抽象 子弹速度属性
    public abstract float FireSpeed { get; }
    //抽象 子弹图标属性
    public abstract Sprite WeaponIcon { get; }

    // Weapon1 没有被实例是如何被调用的
    public Weapon1(Transform shotPos, bool isPlayerWeapon)
    {
        //如何理解这句话？
        this.shotPos = shotPos;
        //现在子弹 为 最大子弹 ——这句话为何要这么写？
        currentBullet = MaxBulletCount;
        //最后开火时间为开火速度——这句话为何要这么写？
        lastFireTime = FireSpeed;
        //这个方法的 isisPlayerWeapon 为 isPlayerWeapon ——这句话为何要这么写？
        this.isPlayerWeapon = isPlayerWeapon;
    }

    //瞄准方法 （存入一个aimpos变量以后用）
    public void Aim(Vector3 aimPos)
    {
        // 方向为 （瞄准点 - 射击位置)归一化为整数
        Vector3 direction = (aimPos - shotPos.position).normalized;
        //射击位置为 设计点的父对象位置 + 方向
        shotPos.position = shotPos.parent.position + direction;
        //向右的射击点为方向——为何要这么写？
        shotPos.right = direction;
     }

    //射击方法
    public void Fire(Transform firePoint)
    {
        //获得父类开火点位置
        Transform parent = firePoint.parent;
        //获得父类指向开火点的位置
        //Vector2 temp = new Vector2(firePoint.position.x - parent.position.x, 0);

        //创建子弹飞 将模板实例化（加载）到场景中
        GameObject go = GameObject.Instantiate(BulletPrefab);

        //设置位置与旋转
        go.transform.position = firePoint.position;
        //子弹飞向前为 开火点向右——为何是开火点向右
        go.transform.right = firePoint.right;
        //子弹 飞标签为父标签——为何要为父标签
        go.tag = parent.tag;

        //给子弹伤害  子弹飞获取Bullet组件下的伤害值 为 武器伤害
        go.GetComponent<Bullet>().damageValue = WeaponDamage;
        //子弹飞获取Bullet组件下的为玩家子弹值为 这个玩家的 武器
        go.GetComponent<Bullet>().isPlayerBullet = this.isPlayerWeapon;

        //给子弹速度，让子弹飞行！！！！！！
        //子弹飞 加载刚体组件的速率 为  开火点的右方向*飞行速度
        go.GetComponent<Rigidbody2D>().velocity = firePoint.right * flySpeed;
        //这句话怎么理解？？
        lastFireTime = Time.time;
    }

    public void ColdDown()// true 冷却结束 可以开枪    false冷却中
    {
        //lastFireTime 为 lastFireTime 加计时器
        lastFireTime += Time.deltaTime;

       // Debug.Log("Timer : " + lastFireTime);
    }

    //是冷却方法？——作用为？
    public bool IsColdDown()
    {
        //返回 开火速度小于等于  lastFireTime —  lastFireTime？？
        return Time.time - lastFireTime >= FireSpeed;
    }

    //开始冷却
    public void StartColdDown()
    {
        //为何赋值为 0  什么作用？
        lastFireTime = 0;
    }


}
