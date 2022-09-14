using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        damage--;
        if (damage == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.localScale -= new Vector3(0.35f, 0, 0);
        }
    }
}
