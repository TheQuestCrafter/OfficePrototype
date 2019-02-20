using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInformation : MonoBehaviour
{
    public int Shrutebucks;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
