using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyOnClic(GameObject go)
    {
        Destroy(go);
    }

    public void DestroyInstrument(GameObject go)
    {
        Spot mySpot = GetSpot();
        mySpot.FreeSpot();
        Destroy(go);
    }

    public Spot GetSpot()
    {
        GameObject[] spots = GameObject.FindGameObjectsWithTag("spot");

        foreach (GameObject spot in spots)
        {
            if (spot.GetComponent<RectTransform>().localPosition == transform.parent.GetComponent<RectTransform>().localPosition)
            {
                return spot.GetComponent<Spot>();
            }
        }
        return null;
    }
}
