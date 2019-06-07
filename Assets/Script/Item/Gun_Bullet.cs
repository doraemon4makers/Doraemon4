using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Bullet : MonoBehaviour
{
    public bool isPlayerBullet;
    public GameObject exPrefab;
    public int damageValue;
    public float speed = 5f;

    private Rigidbody2D rgBody2D;

    private void Start()
    {
        rgBody2D = GetComponent<Rigidbody2D>();
        rgBody2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();

        if (damagable != null)
        {
            if (damagable.isPlayerSide == true && isPlayerBullet == false ||
                damagable.isPlayerSide == false && isPlayerBullet == true)
            {
                damagable.BeDamage(damageValue);
                Debug.Log("Do Instantiate");
                Instantiate(exPrefab, transform.position, transform.rotation);
                Debug.Log("Do Destroy");
                Destroy(gameObject);
            }
        }


    }

}
