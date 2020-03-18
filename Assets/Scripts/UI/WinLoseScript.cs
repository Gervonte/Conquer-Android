using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class WinLoseScript : MonoBehaviour
{
    [SerializeField] private AudioClip buttonHoverSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    public GameObject gameController;
    public Text phrase;
    [SerializeField] private string mainScene = "Main Menu";
    private int timesHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        
       gameController = GameObject.FindGameObjectWithTag("GameController");
       phrase.text += gameController.GetComponent<GameControl>().finalTimerText;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void backButtonHit()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);        
        if(timesHit >=1)
        {
            Destroy(gameController.gameObject);
            SceneManager.LoadScene(mainScene);

        }
        else 
        {
            AdController.Instance.showRegularAd();
            timesHit += 1;
        }
       
    }
    public void buttonHoverSound()
    {
        AudioManager.Instance.PlaySFX(buttonHoverSFX);
    }


}
