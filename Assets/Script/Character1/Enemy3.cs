using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public int hp = 1;
    public bool invincible = false;
    public bool dropWeapon = true;
    private GameObject dropWeaponPrefab;

    private void Start()
    {
        dropWeaponPrefab = Resources.Load<GameObject>("Vehicle/robot");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Hurt(1);
        }
    }

    public void Hurt(int damage)
    {
        if (!invincible)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Die();

            }

        }

    }


    public void Die()
    {
        if (dropWeapon)
        {
            Instantiate(dropWeaponPrefab, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);

    }
}
