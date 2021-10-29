using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientScript : MonoBehaviour
{
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = gradient.Evaluate(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
