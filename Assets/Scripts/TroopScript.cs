using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TroopScript : MonoBehaviour
{

    Rigidbody rb;
    string spawnBase;
    public float speed;
    public float combatSpeed;
    string troopType;
    int troopNumber;

    public float maxHealth;
    public float health;
    public float defense;
    float counter = 0;

    public float attack;
    public bool stop = false;

    public GameObject currentAttack = null;
    public GameObject gameController;
    public GameObject healthBar;

    Animator anim;
    public event Action<float> OnHealthPctChanged = delegate { };

    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask;

    // This would cast rays only against colliders in layer 8.
    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

    RaycastHit hit;
    float maxShootDistance;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        speed += (UnityEngine.Random.Range(-1, 1));
        attack += (UnityEngine.Random.Range(-10, 10));
        defense += (UnityEngine.Random.Range(-5, 5));
        maxHealth = health;
        combatSpeed = speed - 2;
        maxShootDistance = Mathf.Infinity;
        healthBar.SetActive(false);
        if (gameController.GetComponent<GameControl>().isLevelForest)
        {
            maxShootDistance = 5F;
        }

        if (troopType == "Skeleton")
        {
            troopNumber = 0;
        }
        if (troopType == "Knight")
        {
            troopNumber = 1;
        }
        if (troopType == "Soldier")
        {
            troopNumber = 2;
        }
    }
    private void FixedUpdate()
    {
        if (troopType == "Soldier" && currentAttack == null)
        {
            if ((spawnBase == "Base 1" && checkStatus("baseTwo") > 0) || (spawnBase == "Base 2" && checkStatus("baseOne") > 0))
            {
                longRangeAttack();
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        CheckforAttack();
        if (currentAttack)
        {
            stop = true;
        }

        if (!stop)
        {
            rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (health <= 0)
        {
            this.gameObject.GetComponent<soundEffects>().death();
            gameController.GetComponent<GameControl>().troopInCombat[1] = 0;
            gameController.GetComponent<GameControl>().troopInCombat[0] = 0;
            if (spawnBase == "Base 1")
            {
                gameController.GetComponent<GameControl>().coins[1] += 25;
            }
            else if (spawnBase == "Base 2")
            {
                gameController.GetComponent<GameControl>().coins[0] += 25;
                GameObject.FindGameObjectWithTag("AI").GetComponent<AI_Script>().troopsSpawned[troopNumber] -= 1;
            }
            Destroy(gameObject);
        }
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        if (counter <= 0 && currentAttack)
        {
            attackTroop(currentAttack);
            counter = 8.5F - speed;
        }






        Animate();

    }
    private void longRangeAttack()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxShootDistance))
        {
            //            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if ((spawnBase == "Base 1" && hit.collider.gameObject.tag == "baseTwo") ||
                (spawnBase == "Base 2" && hit.collider.gameObject.tag == "baseOne"))
            {
                currentAttack = hit.collider.gameObject;
                if (spawnBase == "Base 1")
                {
                    //Debug.Log("hit");
                    gameController.GetComponent<GameControl>().troopInCombat[0] = 1;
                }
                else if (spawnBase == "Base 2")
                {
                    gameController.GetComponent<GameControl>().troopInCombat[1] = 1;
                }

                stop = true;
            }

        }
        else
        {

        }
    }
    void Animate()
    {
        if (anim.GetBool("isAttacking") == false)
        {
            if (anim.GetBool("isMoving") == true)
            {
                anim.SetInteger("condition", 1);
            }
            else
            {
                anim.SetInteger("condition", 0);
            }
        }
    }
    void CheckforAttack()
    {
        if (spawnBase == "Base 1" && gameController.GetComponent<GameControl>().troopInCombat[0] == 0)
        {
            stop = false;
            //currentAttack = null;
        }
        if (spawnBase == "Base 2" && gameController.GetComponent<GameControl>().troopInCombat[1] == 0)
        {
            stop = false;
            //currentAttack = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {


        if ((spawnBase == "Base 1" && collision.gameObject.tag == "baseTwo") ||
        (spawnBase == "Base 2" && collision.gameObject.tag == "baseOne"))
        {
            currentAttack = collision.gameObject;
            if (spawnBase == "Base 1")
            {
                gameController.GetComponent<GameControl>().troopInCombat[0] = 1;
            }
            else if (spawnBase == "Base 2")
            {
                gameController.GetComponent<GameControl>().troopInCombat[1] = 1;
            }

            stop = true;
        }

        if ((spawnBase == "Base 1" && collision.gameObject.tag == "baseOne") ||
            (spawnBase == "Base 2" && collision.gameObject.tag == "baseTwo"))
        {
            //if ((spawnBase == "Base 1" && gameController.GetComponent<GameControl>().troopInCombat[0] == 0) || spawnBase == "Base 2" && gameController.GetComponent<GameControl>().troopInCombat[1] == 0)
            //{
                Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), this.gameObject.GetComponent<BoxCollider>());
            //}
            //else
            //{
              //  stop = true;
            //}

        }
        if ((spawnBase == "Base 1" && collision.gameObject.tag == "baseTwoInside") ||
            (spawnBase == "Base 2" && collision.gameObject.tag == "baseOneInside"))
        {
            stop = true;
            collision.gameObject.GetComponent<BaseScript>().ModifyHealth((int)health);
            GameObject.FindGameObjectWithTag("AI").GetComponent<AI_Script>().troopsSpawned[troopNumber] -= 1;
            //health = 0;
            Destroy(gameObject);

        }



    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "water")
        {
            if (speed > 2)
            {
                speed -= 2;
            }

        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "water")
        {
            speed += 2;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((spawnBase == "Base 1" && collision.gameObject.tag == "baseOne") ||
             (spawnBase == "Base 2" && collision.gameObject.tag == "baseTwo"))
        {
            if ((spawnBase == "Base 1" && gameController.GetComponent<GameControl>().troopInCombat[0] == 0) || spawnBase == "Base 2" && gameController.GetComponent<GameControl>().troopInCombat[1] == 0)
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), this.gameObject.GetComponent<BoxCollider>());
            }
            else
            {
                stop = true;
            }

        }
        if ((spawnBase == "Base 1" && collision.gameObject.tag == "baseTwo") ||
       (spawnBase == "Base 2" && collision.gameObject.tag == "baseOne"))
        {
            stop = true;
            currentAttack = collision.gameObject;
        }


    }

    public void attackTroop(GameObject troop)
    {
        //stop = true;
        StartCoroutine(attackRoutine(troop));


    }
    public void ModifyHealth(int amount)
    {
        healthBar.SetActive(true);
        health -= amount;
        float currentHealthPct = (float)health / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

    }
    IEnumerator attackRoutine(GameObject troop)
    {
        anim.SetBool("isAttacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("isAttacking", false);
        if (troop)
        {
            troop.GetComponent<soundEffects>().hit();
            if (troop.GetComponent<TroopScript>().health > 0)
            {

                troop.GetComponent<TroopScript>().ModifyHealth((int)(attack - troop.GetComponent<TroopScript>().defense));
            }
        }


    }
    public void setSpawnBase(string newSpawnBase)
    {
        spawnBase = newSpawnBase;
    }
    public void setTroopType(string newTroopType)
    {
        troopType = newTroopType;
    }

    public void setHealth(float newHealth)
    {
        health = newHealth;
    }
    public void setAttack(float newAttack)
    {
        attack = newAttack;
    }
    public void setDefense(float newDefense)
    {
        defense = newDefense;
    }
    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    int checkStatus(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }
}
