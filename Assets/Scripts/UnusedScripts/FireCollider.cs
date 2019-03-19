using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public FireBehavior fireBehaviorScript;
    public GameObject fire;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!fireBehaviorScript.fireIsFullSize)
            {
                fireBehaviorScript.ToggleInterface(true);
                collision.gameObject.SendMessage("IdentifyFire", fire);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//when they leave the area, remove the interface and prevent them from interacting with the fire
    {
        if (collision.gameObject.tag == "Player")
        {
            fireBehaviorScript.ToggleInterface(false);
            collision.gameObject.SendMessage("IdentifyFire", gameObject);//can't send null as a parameter, so sending this object as a dud instead
        }
    }
}
