using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInformation : MonoBehaviour
{
    //0 is worst, 1 is okay/good, 2 is great
    public int chiliWin { get; set; }
    public int jimWin { get; set; }
    public int larryWin { get; set; }

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
