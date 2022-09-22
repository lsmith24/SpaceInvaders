using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;

    public float timer;
    public int score;
    public int lives;
    public int invadersLeft;
    public float spawnPeriod;

    public Vector3 spawnPos;
    public GameObject ship;
    Ship playerShip;
    public GameObject platform;

    public GameObject shield;
    public GameObject invaderLarge;
    public GameObject invaderMed;
    public GameObject invaderSmall;
    public GameObject invaderUFO;
    GameObject currentUFO;
    bool active;
    Invader[,] invaderArr;
    bool moveLeft = true;
    bool won = false;

    public GameObject powerUp;
    public float timer2;
    public float powerUpPeriod;
    public float powerUpDuration;
    public bool powerUpActive;
    public float startTime;
    GameObject currentPowerUp;

    public GameObject scoreUI;
    public GameObject livesUI;
    public GameObject winUI;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        setupGame();
    }

    public void setupGame()
    {
        score = 0;
        timer = 0;
        timer2 = 0;
        lives = 3;
        spawnPeriod = 20.0f;
        powerUpPeriod = Random.Range(5.0f, 10.0f);
        active = false;
        powerUpActive = false;
        powerUpDuration = Random.Range(3.0f, 7.0f);

        setupInvaders();

        GameObject playerObj = Instantiate(ship, spawnPos, Quaternion.identity) as GameObject;
        playerShip = playerObj.GetComponent<Ship>();
        playerShip.global = gameObject;

        GameObject pf = Instantiate(platform, new Vector3(0, 0, -2.5f), Quaternion.identity) as GameObject;

        setupShields();

        GameObject scoreObj = Instantiate(scoreUI, new Vector3(0, 0, 20), Quaternion.identity) as GameObject;
        GameObject livesObj = Instantiate(livesUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

    public void setupShields()
    {
        Vector3 shieldPos = new Vector3(-12.0f, 0.0f, 3.0f);
        Instantiate(shield, shieldPos, Quaternion.identity);
        for (int i = 0; i < 3; i++)
        {
            shieldPos.x += 8.0f;
            Instantiate(shield, shieldPos, Quaternion.identity);
        }
    }

    public void setupInvaders()
    {
        moveLeft = true;
        invaderArr = new Invader[4, 12];
        Vector3 invPos = new Vector3(-15.0f, 0.0f, 15.0f);

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                if (row == 0)
                {
                    // large invaders
                    Vector3 rotation = new Vector3(90, 0, 0);
                    Quaternion quat = Quaternion.Euler(rotation);
                    GameObject invader = Instantiate(invaderLarge, invPos, quat) as GameObject;
                    Invader inv = invader.GetComponent<Invader>();
                    inv.speed = -0.01f;
                    invaderArr[row, col] = inv;
                }
                else if (row == 1 || row ==2)
                {
                    // medium invaders
                    GameObject invader = Instantiate(invaderMed, invPos, Quaternion.identity) as GameObject;
                    Invader inv = invader.GetComponent<Invader>();
                    inv.speed = -0.01f;
                    invaderArr[row, col] = inv;
                }
                else
                {
                    // small invaders
                    GameObject invader = Instantiate(invaderSmall, invPos, Quaternion.identity) as GameObject;
                    Invader inv = invader.GetComponent<Invader>();
                    inv.speed = -0.01f;
                    invaderArr[row, col] = inv;
                }
                invPos.x += 3.0f;
            }
            invPos.x = -15.0f;
            invPos.z += 3.0f;
        }
        invadersLeft = 48;
        invPos.x = -15.0f;
        invPos.z = 15.0f;
    }

    public void spawnUFO()
    {
        Vector3 invPos = new Vector3(-35.0f, 0.0f, 27);
        GameObject invader = Instantiate(invaderUFO, invPos, Quaternion.identity) as GameObject;
        Invader inv = invader.GetComponent<Invader>();
        inv.speed = 0.15f;
        currentUFO = invader;
        active = true;
    }

    public void spawnPowerUp()
    {
        GameObject power = Instantiate(powerUp) as GameObject;
        powerUpActive = true;
        PowerUp pu = power.GetComponent<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invadersLeft == 0)
        {
            won = true;
            gameOver();
        }

        timer += Time.deltaTime;
        if (timer > spawnPeriod && !active)
        {
            timer = 0.0f;
            spawnUFO();
        }

        timer2 += Time.deltaTime;
        if (timer2 > powerUpPeriod && !powerUpActive)
        {
            spawnPowerUp();
            startTime = Time.deltaTime;
            powerUpActive = true;
        }

        if (timer2 > (startTime + powerUpDuration))
        {
            Destroy(currentPowerUp);
            unfreezeInvaders();
            timer2 = 0.0f;
        }

        if (active && currentUFO.transform.position.x > 40)
        {
            Destroy(currentUFO);
            active = false;
        }

        float min = 20.0f;
        float max = -20.0f;

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                if (!invaderArr[row, col].Equals(null))
                {
                    // find min and max invader positions
                    Vector3 pos = invaderArr[row, col].transform.position;
                    bool hit = invaderArr[row, col].hit;
                    if (pos.z <= 0 && !hit)
                    {
                        gameOver();
                    }
                    min = Mathf.Min(pos.x, min);
                    max = Mathf.Max(pos.x, max);
                }
            }
        }

        // check if min and max are global min and max
        if ((min <= -20.0f && moveLeft) || (max >= 20.0f && !moveLeft))
        {
            moveLeft = !moveLeft;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 12; col++)
                {
                    if (!invaderArr[row, col].Equals(null))
                    {
                        bool hit = invaderArr[row, col].hit;
                        if (!hit && !powerUpActive)
                        {
                            Vector3 currPos = invaderArr[row, col].transform.position;
                            currPos.y = 0.0f;
                            currPos.z -= 0.4f;
                            invaderArr[row, col].transform.position = currPos;
                            invaderArr[row, col].speed *= -1.2f;
                        }
                        if (hit)
                            invaderArr[row, col].speed = 0;
                    }
                }
            }
        }

        // fire some percentage of the time
        if (Random.Range(0.0f, 1.0f) <= (0.00004f * invadersLeft) && !powerUpActive)
        {
            int randRow = (int)Random.Range(0.0f, 4.0f);
            int randCol = (int)Random.Range(0.0f, 11.0f);

            invaderArr[randRow, randCol].Fire();
        }

    }

    public void freezeInvaders()
    {
        powerUpActive = false;
 
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                if (!invaderArr[row, col].Equals(null))
                {
                    bool hit = invaderArr[row, col].hit;
                    if (!hit)
                    {
                        invaderArr[row, col].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    }
                }
            }
        }
    }

    void unfreezeInvaders()
    {
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                if (!invaderArr[row, col].Equals(null))
                {
                    bool hit = invaderArr[row, col].hit;
                    if (!hit)
                    {
                        invaderArr[row, col].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    }
                }
            }
        }
    }

    public void gameOver()
    {
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                if (!invaderArr[row, col].Equals(null))
                {
                    Destroy(invaderArr[row, col].gameObject);
                }
            }
        }

        GameObject[] shields;
        shields = GameObject.FindGameObjectsWithTag("Shield");
        foreach(GameObject sh in shields)
        {
            Destroy(sh);
        }

        if (won)
        {
            GameObject winText = Instantiate(winUI, new Vector3(0, 0, 0), Quaternion.identity);
        }
        invadersLeft = 48;
        lives = 3;
        score = 0;
        Destroy(gameObject);
        Application.LoadLevel("GameOver");
    }

    public void loseLife()
    {
        lives--;
        if (lives == 0)
        {
            gameOver();
        }
        else
        {
            GameObject playerObj = Instantiate(ship, spawnPos, Quaternion.identity) as GameObject;
            playerShip = playerObj.GetComponent<Ship>();
            playerShip.global = gameObject;
        }
    }
}
