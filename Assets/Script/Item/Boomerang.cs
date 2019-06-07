using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Boomerang : Weapon1
{
    UIManager ui = new UIManager();

    public Boomerang(Transform shotPos, bool isPlayerWeapon) : base(shotPos, isPlayerWeapon)
    {
    }

    protected override GameObject BulletPrefab
    {
        get
        {
            if (bulletPrefab == null) // 绝对路径 从系统盘开始
                                      //资源加载 1.文件夹名要与Resources一致 2.加载的资源存在内存中 不在场景中 3.输入参数为相对路径（相对某个文件夹的路径）
                Debug.Log("null");
                bulletPrefab = Resources.Load<GameObject>("Bullerts/Play1Bullet");

            Debug.Log(bulletPrefab);

            return bulletPrefab;
        }
    }

    public override float flySpeed

    {
        get
        {
            return 5;
        }
    }

    protected override GameObject GunPrefab
    {
        get
        {
            if (gunPrefab == null)
            {
                gunPrefab = Resources.Load<GameObject>("Weapons/Pistol");
            }

            return gunPrefab;
        }
    }

    public override int MaxBulletCount
    {
        get
        {
            return 15;
        }

    }

    protected override int WeaponDamage { get { return 1; } }

    public override float FireSpeed

    {
        get
        {
            return 0.8f;
        }
    }

    public override Sprite WeaponIcon
    {
        get
        {
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("WeaponIcon");

            return spriteAtlas.GetSprite("G6");
        }
    }
}
