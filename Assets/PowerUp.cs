using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject global;

    public float speed = 1f;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        ChangePosition();
        Physics.IgnoreLayerCollision(7, 8);
        Physics.IgnoreLayerCollision(6, 8);
    }

    void ChangePosition()
    {
        newPos = new Vector3(Random.Range(-30.0f, 30.0f), 0, Random.Range(8.0f, 24.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, newPos) < 1)
        {
            ChangePosition();
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }

    public void Activate()
    {     
        Debug.Log("Freeze");
        Global g = global.GetComponent<Global>();
        g.freezeInvaders();
        Destroy(gameObject);
    }
}
