using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Global globalObj;
    Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        livesText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + globalObj.lives.ToString();
    }
}
