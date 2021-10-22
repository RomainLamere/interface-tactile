using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasScript : MonoBehaviour
{
    public GameObject menu1;
    public GameObject menu2;

    private float time = 0;
    private bool isClicked = false;
    private bool firstTry = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            time += Time.deltaTime;
        }
        print(time);
        if (time > 1)
        {
            if (firstTry)
            {
                menu1.SetActive(true);
                firstTry = false;
                isClicked = false;
                time = 0;
            }
            else
            {
                menu2.SetActive(true);
                firstTry = true;
                isClicked = false;
                time = 0;
            }
            
        }
    }

    public void OnMouseDown()
    {
        time = 0;
        isClicked = true;
    }
    public void OnMouseUp()
    {
        isClicked = false;
    }
}
