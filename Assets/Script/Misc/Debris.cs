using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private GameObject[] debris = new GameObject[2];
    private float y_minSpeed = 10;
    private float y_maxSpeed = 15;
    private float x_speed = 2;
    private GameObject[] bigExplode = new GameObject[6];

    void Start()
    {
        bigExplode[0] = Resources.Load<GameObject>("Prefabs/bigExplode");
        debris[0] = Resources.Load<GameObject>("Prefabs/star0");
        debris[1] = Resources.Load<GameObject>("Prefabs/star6");

        Explode();
        Invoke("SpawnDebris", 0.8f);

    }

   
    void Update()
    {
        
    }

    void Explode()
    {
        Instantiate(bigExplode[0], transform.position, Quaternion.identity);
    }

    void SpawnDebris()
    {       

        GameObject debris1 = Instantiate(debris[0], transform.position, Quaternion.identity);
        debris1.GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_minSpeed);

        GameObject debris2 = Instantiate(debris[0], transform.position, Quaternion.identity);
        debris2.GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_maxSpeed);

        GameObject debris3 = Instantiate(debris[0], transform.position, Quaternion.identity);
        debris3.GetComponent<Rigidbody2D>().velocity = new Vector2(-x_speed, y_minSpeed);

        GameObject debris4 = Instantiate(debris[0], transform.position, Quaternion.identity);
        debris4.GetComponent<Rigidbody2D>().velocity = new Vector2(-x_speed, y_maxSpeed);
    }
}
