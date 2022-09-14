using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int points;
    public float speed;
    //public GameController gameController;
    public GameObject InvaderBullet;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreLayerCollision(8, 9);
    }

    public void Fire()
    {
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.z -= 0.25f;
        Instantiate(InvaderBullet, spawnPos, Quaternion.identity);
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
