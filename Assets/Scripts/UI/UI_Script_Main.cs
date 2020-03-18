using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Script_Main : MonoBehaviour
{
    [SerializeField] private AudioClip Click;
    [SerializeField] private AudioClip TimeTick;
    [SerializeField] private AudioClip buttonHoverSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    public GameObject gameController;
    public Text timerText;
    public Text coinText;
    public Button troop1BTN;
    public Button troop2BTN;
    public Button troop3BTN;
    public Button QuitBTN;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    bool isPaused, isMusicMuted, isSFXMuted;
    public string mainMenu;
    [SerializeField]
    GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = "Main Menu";
    }

    void Update()
    {
        UpdateCoins();
        UpdateTimerUI();
        UpdateButtons();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;

        }
        if (isPaused)
        {
            showPanel(pauseMenu);
            Time.timeScale = 0;
            troop1BTN.interactable = false;
            troop2BTN.interactable = false;
            troop3BTN.interactable = false;
            AdController.Instance.showBannerAd();



        }
        else
        {
            AdController.Instance.hideBannerAd();
            hidePanel(pauseMenu);
            Time.timeScale = 1;
        }

    }
    //call this on update
    public void UpdateTimerUI()
    {
        float old = secondsCount;
        //set timer UI
        secondsCount += Time.deltaTime;
        if ((int)secondsCount > (int)old)
        {
            AudioManager.Instance.PlaySFX(TimeTick);
        }

        if (minuteCount < 10)
        {
            if ((int)secondsCount < 10)
            {
                timerText.text = "0" + minuteCount + " : 0" + (int)secondsCount;
            }
            else
            {
                timerText.text = "0" + minuteCount + " : " + (int)secondsCount;
            }

        }
        else
        {
            if ((int)secondsCount < 10)
            {
                timerText.text = "0" + minuteCount + " : 0" + (int)secondsCount;
            }
            else
            {
                timerText.text = minuteCount + " : " + (int)secondsCount;
            }
        }

        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }

    }

    public void UpdateCoins()
    {
        coinText.text = "$" + gameController.GetComponent<GameControl>().coins[0];
    }
    public void UpdateButtons()
    {
        if (gameController.GetComponent<GameControl>().isCoolDownActive(gameController.GetComponent<GameControl>().baseOneTimer[0]) || gameController.GetComponent<GameControl>().coins[0] < gameController.GetComponent<GameControl>().troopCost[0])
        {

            troop1BTN.interactable = false;
        }
        else
        {
            troop1BTN.interactable = true;
        }

        if (gameController.GetComponent<GameControl>().isCoolDownActive(gameController.GetComponent<GameControl>().baseOneTimer[1]) || gameController.GetComponent<GameControl>().coins[0] < gameController.GetComponent<GameControl>().troopCost[1])
        {

            troop2BTN.interactable = false;
        }
        else
        {
            troop2BTN.interactable = true;
        }

        if (gameController.GetComponent<GameControl>().isCoolDownActive(gameController.GetComponent<GameControl>().baseOneTimer[2]) || gameController.GetComponent<GameControl>().coins[0] < gameController.GetComponent<GameControl>().troopCost[2])
        {

            troop3BTN.interactable = false;
        }
        else
        {
            troop3BTN.interactable = true;
        }
    }
    public void troop1BtnHit()
    {
        if (!isPaused)
        {
            gameController.GetComponent<GameControl>().spawnTroop(1, 0);
            AudioManager.Instance.PlaySFX(Click);
        }

    }

    public void troop2BtnHit()
    {
        if (!isPaused)
        {
            gameController.GetComponent<GameControl>().spawnTroop(1, 1);
            AudioManager.Instance.PlaySFX(Click);
        }
    }
    public void troop3BtnHit()
    {
        if (!isPaused)
        {
            gameController.GetComponent<GameControl>().spawnTroop(1, 2);
            AudioManager.Instance.PlaySFX(Click);
        }
    }
    public void quitButtonHit()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        
        SceneManager.LoadScene(mainMenu);
    }
    public void pauseButtonHit()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        
        isPaused = !isPaused;
    }
    public void musicButtonHit()
    {
        isMusicMuted = !isMusicMuted;
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        if (isMusicMuted)
        {
            AudioManager.Instance.SetMusicVolume(0);
        }
        else
        {
            AudioManager.Instance.SetMusicVolume(0.4F);
        }
    }
    public void sfxButtonHit()
    {
        isSFXMuted = !isSFXMuted;
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        if (isSFXMuted)
        {
            AudioManager.Instance.SetSFXVolume(0);
        }
        else
        {
            AudioManager.Instance.SetSFXVolume(1);
        }

    }
  
    public void buttonHoverSound()
    {
        AudioManager.Instance.PlaySFX(buttonHoverSFX);
    }
    void hidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    void showPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

}
