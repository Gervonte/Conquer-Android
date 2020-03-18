using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.SceneManagement;
public class AdController : MonoBehaviour
{
    #region Instance
    private static AdController instance;
    public static AdController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdController>();
                if (instance==null)
                {
                    instance = new GameObject("Spawned AdController", typeof(AdController)).GetComponent<AdController>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    [Header("Config")]
    [SerializeField] private string gameID = "3397287";
    [SerializeField] private bool testMode = false;
    [SerializeField] private string regularPlacementId = "video";
    [SerializeField] private string bannerPlacementId = "pauseBanner";
    [SerializeField] private GameObject gameController;
    [SerializeField] private string mainScene = "Main Menu";



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gameController = GameObject.FindGameObjectWithTag("GameController");
        Advertisement.Initialize(gameID, testMode); 
        Monetization.Initialize(gameID, testMode);
        Advertisement.Banner.SetPosition(BannerPosition.CENTER);
        
    }
    IEnumerator ShowBannerWhenReady ()
    {
        while(!Advertisement.IsReady(bannerPlacementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerPlacementId);
    }
    public void showBannerAd()
    {
        if(!Advertisement.Banner.isLoaded)
        {
            StartCoroutine(ShowBannerWhenReady());
        }
        else
        {
            Advertisement.Banner.Show();
        }
        
    }
    public void showRegularAd()
    {
        StartCoroutine(WaitForAd());
    }
    IEnumerator WaitForAd()
    {
        
        while (!Monetization.IsReady(regularPlacementId))
        {
            
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(regularPlacementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            
            ad.Show(AdFinished);
        }
    }
    void AdFinished(UnityEngine.Monetization.ShowResult result)
    {
        if (result == UnityEngine.Monetization.ShowResult.Finished)
        {
            Debug.Log("done");
           
        }
        Destroy(gameController.gameObject);
        SceneManager.LoadScene(mainScene);
    }

    public void hideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

}