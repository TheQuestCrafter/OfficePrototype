using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    Vector3 currentPosition;
    [SerializeField]
    float speed;
    [SerializeField]
    bool moveRight = true;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(moveRight)
            currentPosition.x =  speed;
        else
            currentPosition.x =  -(speed);

        transform.position += currentPosition;

        if (Time.fixedTime >= 7)
            gameObject.SetActive(false);


    }
}
