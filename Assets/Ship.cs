using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Space Invaders Ship
    public Vector3 force;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        force.x = 0.07f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.6f;
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            BulletScript b = obj.GetComponent<BulletScript>();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().position += force;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().position += -force;
        }
    }
}
