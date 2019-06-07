using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet;
    public GameObject exPrefab;
    public int damageValue;   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit: " + collision.gameObject.name);

        IDamagable damagable = collision.GetComponent<IDamagable>();

        if(damagable != null)
        {
            if (damagable.isPlayerSide == true && isPlayerBullet == false ||
                damagable.isPlayerSide == false && isPlayerBullet == true)
            {
                damagable.BeDamage(damageValue);
                Instantiate(exPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }


        //Character1 character = collision.gameObject.GetComponent<Character1>();
        ////
        //Play1 player = collision.gameObject.GetComponent<Play1>();//里氏替换原则 子类可以替代父类 而程序功能不受影响

        ////确定撞到的是角色 而且阵营不一样
        //if (player != null && isPlayerBullet == false)
        //{
        //    player.BeDamage(damageValue);
        //    Instantiate(exPrefab, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}

        //if (character != null && isPlayerBullet == true) //确定撞到的是角色 而且阵营不一样
        //{
        //    Debug.Log("Hit Enemy");
        //    character.BeDamage(damageValue);
        //    Instantiate(exPrefab, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}
    }
}
