using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public Global global;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, (Screen.width / 4) * 3));
        if (GUILayout.Button("New Game"))
        {
            Application.LoadLevel("GameScene");
        }
        GUILayout.EndArea();
    }
}
