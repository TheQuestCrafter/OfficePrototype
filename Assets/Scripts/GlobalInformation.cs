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
        //Keeps the original GameMaster if the next scene has a game master.
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
        if (GM.GMActive == true)
        {
            Destroy(this.gameObject);
        }
        GMActive = true;

        //Keeps the GameMaster throughout the game
        DontDestroyOnLoad(this);
    }
}
