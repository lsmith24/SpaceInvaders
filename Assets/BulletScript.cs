using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 thrust;
    public GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        thrust.z = 450.0f;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Invader") || collider.CompareTag("UFO"))
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            invader.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Shield"))
        {
            Shield shield = collider.gameObject.GetComponent<Shield>();
            shield.takeDamage();
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
