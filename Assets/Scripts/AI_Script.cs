using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Script : MonoBehaviour
{
    public GameObject gameController;
    public string baseOne = "baseOne";
    public string baseTwo = "baseTwo";
    public int spawnAmount = 1;
    public int difficulty = 0;
    public int[] troopsSpawned = new int[3];

    float startTimer;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        //        gameController.GetComponent<GameControl>().spawnTroop(2, 0);
        if (difficulty == 0)
        {
            startTimer = 5;
            spawnAmount = 1;
        }
        else if (difficulty == 1)
        {
            startTimer = 3;
            spawnAmount = 2;
        }
        else if (difficulty == 2)
        {
            startTimer = 0;
            spawnAmount = 4;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
        }

        if (checkStatus(baseTwo) < spawnAmount && startTimer <= 0)
        {
            spawnCorrect();
        }
    }
    void spawnCorrect()
    {
        if (difficulty == 0)
        {
            if (gameController.GetComponent<GameControl>().spawnTroop(2, 0) == 1)
            {
                troopsSpawned[0] += 1;
                startTimer = 10;
            }

        }
        else if (difficulty == 1)
        {
            if ((troopsSpawned[2] == 0) && !(gameController.GetComponent<GameControl>().isLevelForest))
            {
                if (gameController.GetComponent<GameControl>().spawnTroop(2, 2) == 1)
                {
                    troopsSpawned[2] += 1;
                    startTimer = 10;
                }
            }
            else
            {
                if (gameController.GetComponent<GameControl>().spawnTroop(2, 0) == 1)
                {
                    troopsSpawned[0] += 1;
                    startTimer = 10;
                }
            }
        }
        else if (difficulty == 2)
        {
            if ((troopsSpawned[2] == 0) && !(gameController.GetComponent<GameControl>().isLevelForest))
            {
                if (gameController.GetComponent<GameControl>().spawnTroop(2, 2) == 1)
                {
                    troopsSpawned[2] += 1;
                    startTimer = 10;
                }
            }
            else if (troopsSpawned[1] == 0)
            {
                if (gameController.GetComponent<GameControl>().spawnTroop(2, 1) == 1)
                {
                    troopsSpawned[1] += 1;
                    startTimer = 10;
                }
            }
            else
            {
                if (gameController.GetComponent<GameControl>().spawnTroop(2, 0) == 1)
                {
                    troopsSpawned[0] += 1;
                    startTimer = 5;
                }
            }
        }
    }
    int checkStatus(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }
}
