using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    Global globalObj;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject g = GameObject.Find("GlobalObject");
        //globalObj = g.GetComponent<Global>();
        //scoreText = gameObject.GetComponent<Text>();
        //scoreText.text = "Final Score: " + globalObj.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            Application.LoadLevel("StartScene");
        }
    }
}
