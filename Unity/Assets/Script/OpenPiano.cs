using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPiano : MonoBehaviour
{
    public GameObject pianoPrefab;
    [SerializeField] GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Distance(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2));
    }
    public void OpenThePiano()
    {
        
        GameObject[] go = GameObject.FindGameObjectsWithTag("spot");
        Vector2 pos1 = parent.GetComponent<RectTransform>().localPosition;
        GameObject mySpot = go[0];


        foreach (GameObject o in go)
        {
            if (Distance(pos1, o.GetComponent<RectTransform>().localPosition) < Distance(pos1, mySpot.GetComponent<RectTransform>().localPosition))
            {
                mySpot = o;
            }
        }
        if (mySpot.GetComponent<Spot>().free)
        {
            mySpot.GetComponent<Spot>().TakeSpot();
            GameObject newPiano = Instantiate(pianoPrefab, parent.transform.parent);
            if (mySpot.GetComponent<RectTransform>().localPosition.y > 0)
            {
                newPiano.GetComponent<RectTransform>().Rotate(0f, 0f, 180f);
            }
            else
            {
                newPiano.GetComponent<RectTransform>().Rotate(0f, 0f, 0f);
            }
            newPiano.GetComponent<RectTransform>().localPosition = mySpot.GetComponent<RectTransform>().localPosition;
            newPiano.GetComponent<HandleInstrument>().spotName = mySpot.name;
            //newPiano.GetComponent<RecordAudio>().SetZone(mySpot.name);
        }
    }
}
