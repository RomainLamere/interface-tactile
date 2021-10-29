using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeScript : MonoBehaviour
{
    bool up = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            
        if(up)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.003f, gameObject.transform.localScale.y + 0.003f, 0);
            if (gameObject.transform.localScale.x > 3)
            {
                up = false;
            }
        }
        else
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.003f, gameObject.transform.localScale.y - 0.003f, 0);
            if (gameObject.transform.localScale.x < 2)
            {
                up = true;
            }
        }
    }

}
