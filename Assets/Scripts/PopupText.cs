using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{

    public Text popupText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);//hide the popup when the game starts
    }

    public void DisplayPopupText(string textToDisplay, float displayTime=3)
    {
        gameObject.SetActive(true);
        popupText.text = textToDisplay;
        StartCoroutine(HidePopupTextCoroutine(displayTime));
    }

    IEnumerator HidePopupTextCoroutine(float waitTimeInSeconds)
    {
        yield return new WaitForSeconds(waitTimeInSeconds);
        gameObject.SetActive(false);
    }
}
