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
        if(GetComponent<RectTransform>().localPosition.y <= -32)
        {
            print("rouge");
            GetComponent<Image>().color = gradient.Evaluate(0);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -32 && GetComponent<RectTransform>().localPosition.y <= -28)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.12f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -28 && GetComponent<RectTransform>().localPosition.y <= -19)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.3f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -19 && GetComponent<RectTransform>().localPosition.y <= -13)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.45f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -13 && GetComponent<RectTransform>().localPosition.y <= -4)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.65f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > -4 && GetComponent<RectTransform>().localPosition.y <= 1)
        {
            GetComponent<Image>().color = gradient.Evaluate(0.8f);
        }
        else if(GetComponent<RectTransform>().localPosition.y > 1)
        {
            GetComponent<Image>().color = gradient.Evaluate(1f);
        }
    }
}
