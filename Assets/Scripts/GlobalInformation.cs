using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInformation : MonoBehaviour
{
    public int Shrutebucks;
    private bool GMActive;
    private GlobalInformation GM;
    void Start()
    {
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
        if (GM.GMActive == true)
        {
            Destroy(this.gameObject);
        }
        GMActive = true;
        DontDestroyOnLoad(this);
    }
}
