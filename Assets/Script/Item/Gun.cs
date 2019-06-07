using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Gun : Weapon1
{
    // 枪方法（射击点，是否玩家武器）                    //base？？ 作用
    public Gun(Transform shotPos, bool isPlayerWeapon) : base(shotPos, isPlayerWeapon)
    {
    }

    // 飞行速度虚方法 
    public override float flySpeed
    {
        //读取一个5的速度  ——不是应该在set上 设置一个5的速度吗  为何是在get上
        get
        {
            return 5;
        }
    }

    //  最大子弹数量的 虚方法 和属性 ——为何又是方法又是属性？
    public override int MaxBulletCount { get {return 15; } }

    // 武器图标
    public override Sprite WeaponIcon
    {
        get
        {
            if (weaponIcon == null)
            {
                weaponIcon = Resources.Load<Sprite>("");
            }

            return weaponIcon ;
        }
    }

    //向下继承的 子弹图片 虚方法
    protected override GameObject GunPrefab
    {
        //读取
        get
        {
            //如果 子弹图片为空
            if (gunPrefab == null)
            {
                //则执行 子弹图片 为  Resources 读取对象 （） —— 这个的作用是？
                gunPrefab = Resources.Load<GameObject>("");
            }

            //返回枪图片值
            return gunPrefab;
        }
    }

    //向下保护层级的 武器伤害的需方法 返回值为2
    protected override int WeaponDamage { get { return 2; } }

    // 子弹图片虚方法 为何有两个这个方法呀 ？可否删掉一个
    protected override GameObject BulletPrefab
    {

        get
        {
            if (bulletPrefab == null)
            {
                bulletPrefab = Resources.Load<GameObject>("Bullerts/PlayerBullet");
            }

            return bulletPrefab;
        }
    }

    //子弹速度 虚方法属性  返回为0.99  为何是要这个数？
    public override float FireSpeed { get { return 0.99f; } }

    //public override void Use(Transform target)
    //{
    //   


    //}
}
