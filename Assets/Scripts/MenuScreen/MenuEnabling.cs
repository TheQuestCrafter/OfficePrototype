using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuEnabling : MonoBehaviour
{
    [Tooltip("Gives the event system to the script to find buttons.")]
    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private GameObject selectedButton;

    private bool buttonSelected;

    public void ReEnableMenu()
    {
        eventSystem.SetSelectedGameObject(selectedButton);
        buttonSelected = true;
    }
}
