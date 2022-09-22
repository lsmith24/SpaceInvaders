using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int points;
    public float speed;
    public GameObject InvaderBullet;
    public bool hit = false;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(6, 6);
    }

    public void Fire()
    {
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.z -= 0.25f;
        Instantiate(InvaderBullet, spawnPos, Quaternion.identity);
    }

    void FixedUpdate()
    {
        if (!hit)
        {
            gameObject.transform.Translate(speed, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.z <= -5)
        {
            Die();
        }
    }

    public void GetHit()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        hit = true;
        Instantiate(explosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));
    }

    public void Die()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        if (gameObject.CompareTag("Invader"))
        {
            g.invadersLeft--;
        }
        g.score += points;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Shield") && !hit)
        {
            Destroy(collider.gameObject);
        }
    }
}
