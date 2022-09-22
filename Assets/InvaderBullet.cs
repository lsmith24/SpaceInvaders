using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBullet : MonoBehaviour
{
    public Vector3 thrust;
    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        thrust.z = -300.0f;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(7, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z <= -5)
        {
            Destroy(gameObject);
        }
        if (gameObject.transform.position.z < 0)
        {
            GetHit();
        }
    }

    public void GetHit()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        hit = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship") && !hit)
        {
            Ship ship = collider.gameObject.GetComponent<Ship>();
            ship.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Bullet") && !hit)
        {
            BulletScript playerBullet = collider.gameObject.GetComponent<BulletScript>();
            playerBullet.GetHit();
            GetHit();
        }
        else if (collider.CompareTag("Shield") && !hit)
        {
            Shield shield = collider.gameObject.GetComponent<Shield>();
            shield.takeDamage();
            GetHit();
        }
        else if (collider.CompareTag("Platform") && !hit)
        {
            GetHit();
        }
        else if (collider.CompareTag("Invader") && !hit)
        {
            GetHit();
        }
    }
}
