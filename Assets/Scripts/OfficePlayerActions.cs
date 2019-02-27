using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficePlayerActions : MonoBehaviour
{
    GameObject fire;//if the player enters the fire's radius, the fire is assigned in IdentifyFire

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StampOutFire();
        }
    }

    void StampOutFire()
    {
        if (fire)//sanity check
        {
            if (fire.tag == "Fire")//make sure it's not trying to stamp out the dud object we send as a null parameter when exiting the fire radius
            {
                fire.SendMessage("ReduceFire");
            }
        }
    }

    void IdentifyFire(GameObject receivedFire)
    {
        fire = receivedFire;
    }
}
