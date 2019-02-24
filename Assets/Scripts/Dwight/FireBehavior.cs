using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{

    public GameObject fireSprite;

    [SerializeField]
    float fireSize = 0.1f;//as the fire grows in size, add visual effects to make it more noticeable
                          //once the fire grows to its full size of 1, the object burns down and the player fails 
                          //the object's scale is attached to the fireSize. at 0.1 it is 10% of its total size
    [SerializeField]
    float fireSizeIncreaseRate = 0.1f;//how quickly the fire grows in size
                                      //a value of 0.1f makes it take about 9 seconds to grow to full size

    bool fireIsFullSize = false;

    [SerializeField]
    float rotationSpeed = 60;

    void Update()
    {
        if (fireSize >= 1 && !fireIsFullSize)//if the fire has finished growing, the player has failed
        {
            EndFire();
        }
        else if(fireSize < 1)
        {
            GrowFire();
        }
    }

    void GrowFire()
    {
        //rotate the fire as a primitive way of making it stand out to the player
        transform.Rotate(0, 0, 1 * rotationSpeed * Time.deltaTime);

        //calculate the new size of the fire over time
        fireSize += fireSizeIncreaseRate * Time.deltaTime;
        transform.localScale = new Vector3(fireSize, fireSize, fireSize);
    }

    void EndFire()
    {
        fireSprite.GetComponent<Renderer>().material.color = Color.black;
        fireIsFullSize = true;
    }
}
