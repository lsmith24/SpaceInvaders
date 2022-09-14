using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Space Invaders Ship
    public float speed;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
        Vector3 shootPos = gameObject.transform.position;
        shootPos.z += 0.51f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.55f;
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            BulletScript b = obj.GetComponent<BulletScript>();
            b.ship = gameObject;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && gameObject.transform.position.x < 18.0f)
        {
            gameObject.transform.Translate(speed, 0, 0);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && gameObject.transform.position.x > -18.0f)
        {
            gameObject.transform.Translate(-speed, 0, 0);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
