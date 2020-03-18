using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image foregroundImage;
    float updateSpeedSeconds = .5f;
    float healthChange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        GetComponentInParent<TroopScript>().OnHealthPctChanged += HealthChanged;
    }
    void HealthChanged(float dif)
    {
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
