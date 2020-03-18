using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public string finalTimerText;
    Vector3 baseOneSpawnPos, baseTwoSpawnPos;
    Vector3 baseOneRot, baseTwoRot;
    public GameObject[] troops = new GameObject[3];
    public string winScene, loseScene;

    int baseOne = 1;
    int baseTwo = 2;

    int[] coolDownTimes = new int[3];
    string[] troopTypes = new string[3];

    public float[] baseOneTimer = new float[3];
    public float[] baseTwoTimer = new float[3];

    public float[] troopSpeed = new float[3];
    public float[] troopHealth = new float[3];
    public float[] troopAttack = new float[3];
    public float[] troopDefense = new float[3];

    public int[] troopInCombat = new int[2];

    public int[] coins = new int[2];
    public float coinTimer;
    public int[] troopCost = new int[3];
    [SerializeField] private AudioClip Money;
    [SerializeField] private AudioClip winStinger;
    [SerializeField] private AudioClip loseStinger;
    public bool isLevelForest = false;

    // Start is called before the first frame update
    void Start()
    {
        initialzeVariables();
    }
    void initialzeVariables()
    {
        baseOneSpawnPos = new Vector3(-27.73155F, 0.1F, 1.59F);
        baseOneRot = new Vector3(0, 90, 0);

        baseTwoSpawnPos = new Vector3(7.800205F, 0.1F, 1.59F);
        baseTwoRot = new Vector3(0, -90, 0);

        troopInCombat[0] = 0;
        troopInCombat[1] = 0;
        coins[0] = 200;
        coins[1] = 200;
        coinTimer = 2;

        //Skeleton
        troopCost[0] = 50;
        troopTypes[0] = "Skeleton";
        baseOneTimer[0] = -1F;
        baseTwoTimer[0] = -1F;
        coolDownTimes[0] = 1;
        troopSpeed[0] = 6F;
        troopHealth[0] = 350;
        troopAttack[0] = 180;
        troopDefense[0] = 50;

        //Knight
        troopCost[1] = 125;
        troopTypes[1] = "Knight";
        coolDownTimes[1] = 2;
        baseOneTimer[1] = -1F;
        baseTwoTimer[1] = -1F;
        troopSpeed[1] = 5.5F;
        troopHealth[1] = 575;
        troopAttack[1] = 250;
        troopDefense[1] = 60;

        //Soldier
        troopCost[2] = 75;
        troopTypes[2] = "Soldier";
        baseOneTimer[2] = -1F;
        baseTwoTimer[2] = -1F;
        coolDownTimes[2] = 1;
        troopSpeed[2] = 7F;
        troopHealth[2] = 250;
        troopAttack[2] = 200;
        troopDefense[2] = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDownActive(baseOneTimer[0]))
        {
            baseOneTimer[0] -= Time.deltaTime;
        }
        if (isCoolDownActive(baseOneTimer[1]))
        {
            baseOneTimer[1] -= Time.deltaTime;
        }
        if (isCoolDownActive(baseOneTimer[2]))
        {
            baseOneTimer[2] -= Time.deltaTime;
        }
        if (isCoolDownActive(baseTwoTimer[0]))
        {
            baseTwoTimer[0] -= Time.deltaTime;
        }
        if (isCoolDownActive(baseTwoTimer[1]))
        {
            baseTwoTimer[1] -= Time.deltaTime;
        }
        if (isCoolDownActive(baseTwoTimer[2]))
        {
            baseTwoTimer[2] -= Time.deltaTime;
        }
        if (isCoolDownActive(coinTimer))
        {
            coinTimer -= Time.deltaTime;
        }
        else
        {
            coins[0] += 10;
            coins[1] += 10;
            //AudioManager.Instance.PlaySFX(Money);
            coinTimer = 2;
        }
    }

    public bool isCoolDownActive(float timer)
    {
        if (timer <= 0)
        {
            timer = -1;
            return false;
        }
        return true;
    }

    public int spawnTroop(int spawnBase, int troopType)
    {

        if (spawnBase == baseOne &&
        baseOneTimer[troopType] <= 0 &&
        coins[0] >= troopCost[troopType])
        {
            GameObject temp = Instantiate(troops[troopType], baseOneSpawnPos, Quaternion.Euler(baseOneRot));
            temp.gameObject.tag = "baseOne";
            temp.GetComponent<TroopScript>().setSpawnBase("Base 1");
            temp.GetComponent<TroopScript>().setTroopType(troopTypes[troopType]);
            temp.GetComponent<TroopScript>().setSpeed(troopSpeed[troopType]);
            temp.GetComponent<TroopScript>().setHealth(troopHealth[troopType]);
            temp.GetComponent<TroopScript>().setAttack(troopAttack[troopType]);
            temp.GetComponent<TroopScript>().setDefense(troopDefense[troopType]);
            baseOneTimer[troopType] = coolDownTimes[troopType];
            coins[0] -= troopCost[troopType];
            return 1;
        }

        if (spawnBase == baseTwo &&
        baseTwoTimer[troopType] <= 0 &&
        coins[1] >= troopCost[troopType])
        {
            GameObject temp = Instantiate(troops[troopType], baseTwoSpawnPos, Quaternion.Euler(baseTwoRot));
            temp.gameObject.tag = "baseTwo";
            temp.GetComponent<TroopScript>().setSpawnBase("Base 2");
            temp.GetComponent<TroopScript>().setTroopType(troopTypes[troopType]);
            temp.GetComponent<TroopScript>().setTroopType(troopTypes[troopType]);
            temp.GetComponent<TroopScript>().setSpeed(troopSpeed[troopType]);
            temp.GetComponent<TroopScript>().setHealth(troopHealth[troopType]);
            temp.GetComponent<TroopScript>().setAttack(troopAttack[troopType]);
            temp.GetComponent<TroopScript>().setDefense(troopDefense[troopType]);
            baseTwoTimer[troopType] = coolDownTimes[troopType];
            coins[1] -= troopCost[troopType];
            return 1;
        }
        return 0;
    }

    public void loseGame(string loseBase)
    {
        DontDestroyOnLoad(this.gameObject);
        finalTimerText = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Script_Main>().timerText.text;
        if (loseBase == "Base 1")
        {

            AudioManager.Instance.PlaySFX(loseStinger);
            SceneManager.LoadScene(loseScene);
        }
        else if (loseBase == "Base 2")
        {
            AudioManager.Instance.PlaySFX(winStinger);
            SceneManager.LoadScene(winScene);
        }
    }


}

