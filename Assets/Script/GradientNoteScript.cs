using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientNoteScript : MonoBehaviour
{
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(GetComponent<RectTransform>().localPosition.y);
        if(GetComponent<RectTransform>().localPosition.y <= -50)
        {
            print("rouge");
            GetComponent<Image>().color = gradient.Evaluate(0);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -50 && GetComponent<RectTransform>().localPosition.y <= -40)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.12f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -40 && GetComponent<RectTransform>().localPosition.y <= -30)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.3f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -30 && GetComponent<RectTransform>().localPosition.y <= -20)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.45f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -20 && GetComponent<RectTransform>().localPosition.y <= -10)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.65f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -10 && GetComponent<RectTransform>().localPosition.y <= 0)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.8f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > 0 && GetComponent<RectTransform>().localPosition.y <= 10)
        {
            GetComponent<Image>().color = gradient.Evaluate(1f);
        }
    }
}
