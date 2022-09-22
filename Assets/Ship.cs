using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Space Invaders Ship
    public float speed;
    public GameObject bullet;
    public GameObject global;
    public AudioClip deathKnell;
    public AudioClip shoot;
    //public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.03f;
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
            AudioSource.PlayClipAtPoint(shoot, gameObject.transform.position);
            BulletScript b = obj.GetComponent<BulletScript>();
            b.ship = gameObject;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && gameObject.transform.position.x < 18.0f)
        {
            gameObject.transform.Translate(speed, 0, 0);
        }
        if (Input.GetAxisRaw("Horizontal") <= 0 && gameObject.transform.position.x > -18.0f)
        {
            gameObject.transform.Translate(-speed, 0, 0);
        }
    }

    public void Die()
    {
        //Instantiate(explosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        Global g = global.GetComponent<Global>();
        g.loseLife();
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Collider collider = collision.collider;
        //if (collider.CompareTag("Invader"))
        //{
        //    Global g = global.GetComponent<Global>();
        //    g.gameOver();
        //    Destroy(gameObject);
        //}
    }

}


