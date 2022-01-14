using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordScript : MonoBehaviour
{
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 50f/255, 50f/255, 1);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1 & gameObject.GetComponent<SpriteRenderer>().color.a == 1)
        {
            time = 0;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 50f / 255, 50f / 255, 0.5f);
        }
        if (time > 1 & gameObject.GetComponent<SpriteRenderer>().color.a == 0.5f)
        {
            time = 0;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 50f / 255, 50f / 255, 1f);
        }
    }
}
