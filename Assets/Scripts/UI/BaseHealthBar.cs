using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
    [SerializeField]
    Image foregroundImage;

    [SerializeField]
    Text foregroundText;

    [SerializeField]
    GameObject basePrefab;
    float updateSpeedSeconds = .5f;
    float healthChange;
    public float healthPercent;
    // Start is called before the first frame update
    void Start()
    {
        healthPercent = ((float)basePrefab.GetComponent<BaseScript>().health / (float)basePrefab.GetComponent<BaseScript>().maxHealth) * 100;
        foregroundText.text = healthPercent + "%";
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        basePrefab.GetComponent<BaseScript>().OnHealthPctChanged += HealthChanged;

    }
    void HealthChanged(float dif)
    {
        healthPercent = ((float)basePrefab.GetComponent<BaseScript>().health / (float)basePrefab.GetComponent<BaseScript>().maxHealth) * 100;
        foregroundText.text = healthPercent + "%";
        StartCoroutine(ChangeToPct(dif));

    }

    private IEnumerator ChangeToPct(float dif)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, dif, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = dif;
    }
}
