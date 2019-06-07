using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunIns : GroupableItem
{
    public Transform shootPoint;
    // 发射间隔
    public float shootF = 0.02f;
    public bool canShoot { get { return bulletPrefab != null && shootPoint != null && Time.time - lastShootTime > shootF; } }

    private GameObject bulletPrefab;

    private Vector3 forwardZero = Vector3.right + Vector3.up;
    private float lastShootTime = 0;

    public override SlotType slotType
    {
        get
        {
            return SlotType.LeftHand;
        }
    }


    protected override void Start()
    {
        base.Start();

        //SetEnglishName("gun");
        ReadWordDictionary.SetChineseAndEnglishName(this);

        MissionSystem1.UpdateMission("M1");

       GetComponent<ZuHu>(). zuheDic.Add("boat", "robot");

        bulletPrefab = Resources.Load<GameObject>("Bullerts/gun_bullet");
    }

    public override void Use(Transform target)
    {
        if (canShoot)
        {
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePositionInScene = Vector3.Scale(mousePositionInWorld, forwardZero);

            Vector3 toMousePositionDir = (mousePositionInScene - target.position).normalized;
            StartCoroutine(ShootWhenPointTo(target.right, toMousePositionDir));
            lastShootTime = Time.time;
        }
    }

    private IEnumerator ShootWhenPointTo(Vector3 targetRight, Vector3 toMousePositionDir)
    {
        float angleShootPointAndMouseNew = Vector3.Angle(shootPoint.right, toMousePositionDir);
        float angleShootPointAndMouseOld = Vector3.Angle(shootPoint.right, toMousePositionDir);
        //Debug.Log("angleShootPointAndMouseNew = " + angleShootPointAndMouseNew);
        while (angleShootPointAndMouseOld > 5 && angleShootPointAndMouseNew <= angleShootPointAndMouseOld)
        {
            angleShootPointAndMouseOld = angleShootPointAndMouseNew;
            angleShootPointAndMouseNew = Vector3.Angle(shootPoint.right, toMousePositionDir);
            //Debug.Log("angleShootPointAndMouseNew = " + angleShootPointAndMouseNew);
            yield return null;
        }
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }



}
