using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private Vector3 pPosition;

    // Update is called once per frame
    void Update()
    {
        pPosition = this.transform.position;
        ControlPaddle();
    }

    private void ControlPaddle()
    {
        float yMove = Input.GetAxis("Vertical");
        var move = new Vector3(0, yMove,0);
        transform.position += move * Speed * Time.deltaTime;
    }



}
