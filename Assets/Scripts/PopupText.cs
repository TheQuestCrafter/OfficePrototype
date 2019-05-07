using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{

    public Text popupText;
    public Image bronzeMedal;
    public Image silverMedal;
    public Image goldMedal;
    public AudioSource winSound;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);//hide the popup when the game starts
    }

    public void DisplayPopupText(string textToDisplay, int medalTier, float displayTime=3)
    {
        gameObject.SetActive(true);
        popupText.text = textToDisplay;
        StartCoroutine(HidePopupTextCoroutine(displayTime));
        if(medalTier <= 0)
        {
            //show no medal
            bronzeMedal.enabled = false; silverMedal.enabled = false; goldMedal.enabled = false;
        }
        else if(medalTier == 1)
        {
            winSound.Play();
            bronzeMedal.enabled = false; silverMedal.enabled = false; goldMedal.enabled = true;
        }
        else if (medalTier == 2)
        {
            winSound.Play();
            bronzeMedal.enabled = false; silverMedal.enabled = true; goldMedal.enabled = false;
        }
        else if (medalTier == 3)
        {
            winSound.Play();
            bronzeMedal.enabled = true; silverMedal.enabled = false; goldMedal.enabled = false;
        }
    }

    IEnumerator HidePopupTextCoroutine(float waitTimeInSeconds)
    {
        yield return new WaitForSeconds(waitTimeInSeconds);
        gameObject.SetActive(false);
    }
}
