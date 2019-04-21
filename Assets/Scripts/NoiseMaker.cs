using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    public float minTimeBetweenNoise = 10;
    public float maxTimeBetweenNoise = 20;
    public List<AudioSource> noiseList = new List<AudioSource>();

    bool canPlayNoise = false;
    bool waiting = false;//don't run the waiting coroutine while it's currently running

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!canPlayNoise && !waiting)
        {
            StartCoroutine(WaitCoroutine());
        }
        else if(canPlayNoise)
        {
            canPlayNoise = false;
            AudioSource noiseToPlay = noiseList[Random.Range(0, noiseList.Count)];
            noiseToPlay.Play();
        }
    }

    IEnumerator WaitCoroutine()
    {
        waiting = true;
        float timeBetweenNoise = Random.Range(minTimeBetweenNoise, maxTimeBetweenNoise);
        yield return new WaitForSeconds(timeBetweenNoise);
        waiting = false;
        canPlayNoise = true;

    }
}
