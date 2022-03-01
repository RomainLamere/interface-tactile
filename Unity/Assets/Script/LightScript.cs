using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightScript : MonoBehaviour
{
    bool start = false;
    public GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
        HandleCircle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoStart()
    {
        start = true;
    }

    public void HandleCircle()
    {
        GameObject go;
        for(int i = 0; i < 8; i++)
        {
            go = Instantiate(circle, this.transform);
            go.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(-3f, 3f), Random.Range(-1f, 1f));
            go.SetActive(true);
        }
    }
     

}
