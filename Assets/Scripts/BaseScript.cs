using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public GameObject gameController;
    [SerializeField] private AudioClip EnteredBase;

    public float maxHealth;
    public float health;
    public string Base;
    public event Action<float> OnHealthPctChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        maxHealth = 1600;
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameController.GetComponent<GameControl>().loseGame(Base);
        }
    }
    public void ModifyHealth(int amount)
    {
        //healthBar.SetActive(true);
        health -= amount;
        float currentHealthPct = (float)health / (float)maxHealth;
        AudioManager.Instance.PlaySFX(EnteredBase);
        OnHealthPctChanged(currentHealthPct);

    }
}
