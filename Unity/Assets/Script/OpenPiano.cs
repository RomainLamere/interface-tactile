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

    public void OpenThePiano()
    {
        GameObject newPiano = Instantiate(pianoPrefab,parent.transform.parent);
        print("scale " + pianoPrefab.GetComponent<RectTransform>().sizeDelta.x);
        if(parent.GetComponent<RectTransform>().eulerAngles.z == 90)
        {
            newPiano.GetComponent<RectTransform>().Rotate(0f, 0f, 90f);
            newPiano.GetComponent<RectTransform>().localPosition = new Vector3(760, 0f, 0f);
        }
        else
        {
            newPiano.GetComponent<RectTransform>().Rotate(0f, 0f, -90f);
            newPiano.GetComponent<RectTransform>().localPosition = new Vector3(-760, 0f, 0f);
        }
    }
}
