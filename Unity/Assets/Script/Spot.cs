using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public bool free = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeSpot()
    {
        free = false;
    }
    public void FreeSpot()
    {
        free = true;
    }
}
