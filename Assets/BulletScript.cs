using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 thrust;
    public GameObject ship;
    public bool hit = false;

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
        if (gameObject.transform.position.z <= -5)
        {
            Destroy(gameObject);
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
        if (collider.CompareTag("Invader") || collider.CompareTag("UFO") && !hit)
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            invader.GetHit();
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
        else if (collider.CompareTag("PowerUp") && !hit)
        {
            Debug.Log("Power Up");
            PowerUp powerUp = collider.gameObject.GetComponent<PowerUp>();
            powerUp.Activate();
            GetHit();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
