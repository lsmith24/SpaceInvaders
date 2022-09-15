using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBullet : MonoBehaviour
{
    public Vector3 thrust;

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
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Ship"))
        {
            Ship ship = collider.gameObject.GetComponent<Ship>();
            ship.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Bullet"))
        {
            BulletScript playerBullet = collider.gameObject.GetComponent<BulletScript>();
            playerBullet.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Shield"))
        {
            Shield shield = collider.gameObject.GetComponent<Shield>();
            shield.takeDamage();
            Destroy(gameObject);
        }
    }
}
