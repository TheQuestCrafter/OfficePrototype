using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBehavior : MonoBehaviour
{

    public GameObject fireSprite;
    public GameObject fireSpawner;
    FireSpawning fireSpawningScript;
    public Image progressBar;
    public Canvas fireCanvas;

    AudioSource audioSource;
    public AudioClip fire_sizzle_out;
    public AudioClip[] fire_extinguish;

    [SerializeField]
    float fireSize = 0.1f;//as the fire grows in size, add visual effects to make it more noticeable
                          //once the fire grows to its full size of 1, the object burns down and the player fails 
                          //the object's scale is attached to the fireSize. at 0.1 it is 10% of its total size
    [SerializeField]
    float fireSizeIncreaseRate = 0.5f;//how quickly the fire grows in size
                                      //a value of 0.1f makes it take about 9 seconds to grow to full size

    [SerializeField]
    float fireSizeDecreaseRate = 0.1f;//how much of the fire the player stamps out with each button press

    [HideInInspector]
    public bool fireIsFullSize = false;

    [SerializeField]
    float rotationSpeed = 60;

    private void Start()
    {
        fireSpawner = GameObject.Find("FireSpawnLocations");
        ToggleInterface(false);
        audioSource = GetComponent<AudioSource>();
        fireSpawningScript = fireSpawner.GetComponent<FireSpawning>();
    }

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
        //fireSprite.transform.Rotate(0, 0, 1 * rotationSpeed * Time.deltaTime);

        //calculate the new size of the fire over time
        fireSize += fireSizeIncreaseRate * Time.deltaTime;
        fireSprite.transform.localScale = new Vector3(fireSize*4, fireSize * 4, fireSize * 4);

        //update the bar showing the player how close to burning down the object is
        progressBar.fillAmount = fireSize;
    }

    void EndFire()
    {
        fireSprite.GetComponent<Renderer>().material.color = Color.black;
        fireSprite.GetComponent<Animator>().enabled = false;
        fireSpawningScript.UpdateFireTally(false);
        audioSource.PlayOneShot(fire_sizzle_out);
        fireIsFullSize = true;
        fireCanvas.enabled = false;
        StartCoroutine(WaitToRemoveFireCoroutine());
        ToggleInterface(false);
    }

    IEnumerator WaitToRemoveFireCoroutine()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void ReduceFire()
    {
        if(fireIsFullSize)
        {
            return;//fire has already reached full size, player has failed
        }
        fireSize -= fireSizeDecreaseRate;
        progressBar.fillAmount = fireSize;
        audioSource.PlayOneShot(fire_extinguish[Random.Range(0, fire_extinguish.Length)]);//plays a random noise from the array of fire extinguish noises
        if(fireSize <= 0)
        {
            //the player has put out the fire
            fireSpawningScript.UpdateFireTally(true);
            Destroy(gameObject);
        }
    }

    public void ToggleInterface(bool showInterface)
    {
        if(showInterface)
        {
            fireCanvas.enabled = true;
        }
        else if(!showInterface)
        {
            fireCanvas.enabled = false;
        }
    }
}
