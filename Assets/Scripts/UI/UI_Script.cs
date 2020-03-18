using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public string forestScene, oceanScene, desertScene;
    public GameObject mainPanel, selectionPanel, directionsPanel;
    public int[] difficulty = new int[3];
    public bool difficultySelected = false;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip buttonHoverSFX;
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
    [SerializeField] private AudioClip music3;
    [SerializeField] private AudioClip music4;

    public int levelSelected;


    // Start is called before the first frame update
    void Start()
    {
        AdController.Instance.hideBannerAd();
        showPanel(mainPanel);
        hidePanel(directionsPanel);
        hidePanel(selectionPanel);
        AudioManager.Instance.PlayMusic(music1);
        AudioManager.Instance.SetMusicVolume(.4F);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startButtonHit()
    {
        hidePanel(mainPanel);
        showPanel(selectionPanel);
        AudioManager.Instance.PlaySFX(buttonClickSFX);

    }
    public void nextButtonHit()
    {
        hidePanel(selectionPanel);
        showPanel(directionsPanel);
        AudioManager.Instance.PlaySFX(buttonClickSFX);
    }
    void hidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    void showPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void easyButton()
    {
        difficulty[0] = 1;
        difficulty[1] = 0;
        difficulty[2] = 0;
        PlayerPrefs.SetInt("Difficulty", 0);
        difficultySelected = true;
        AudioManager.Instance.PlaySFX(buttonClickSFX);
    }
    public void normalButton()
    {
        difficulty[0] = 0;
        difficulty[1] = 1;
        difficulty[2] = 0;
        PlayerPrefs.SetInt("Difficulty", 1);
        difficultySelected = true;
        AudioManager.Instance.PlaySFX(buttonClickSFX);
    }
    public void proButton()
    {
        difficulty[0] = 0;
        difficulty[1] = 0;
        difficulty[2] = 1;
        PlayerPrefs.SetInt("Difficulty", 2);
        difficultySelected = true;
        AudioManager.Instance.PlaySFX(buttonClickSFX);
    }

    public void forestButton()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        if (difficultySelected == true)
        {
            levelSelected = 0;
            hidePanel(selectionPanel);
            showPanel(directionsPanel);
        }

    }
    public void oceanButton()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        if (difficultySelected == true)
        {
            levelSelected = 1;
            hidePanel(selectionPanel);
            showPanel(directionsPanel);
        }
    }
    public void desertButton()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        if (difficultySelected == true)
        {
            levelSelected = 2;
            hidePanel(selectionPanel);
            showPanel(directionsPanel);
        }
    }
    public void returnToMain()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        hidePanel(selectionPanel);
        showPanel(mainPanel);
    }
    public void playButtonHit()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        if (levelSelected == 0)
        {
            AudioManager.Instance.PlayMusicWithCrossFade(music2);
            SceneManager.LoadScene(forestScene);
        }
        if (levelSelected == 1)
        {
            AudioManager.Instance.PlayMusicWithCrossFade(music3);
            SceneManager.LoadScene(oceanScene);
        }
        if (levelSelected == 2)
        {
            AudioManager.Instance.PlayMusicWithCrossFade(music4);
            SceneManager.LoadScene(desertScene);
        }
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void buttonHoverSound()
    {
        AudioManager.Instance.PlaySFX(buttonHoverSFX);
    }
}
