using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleColor : MonoBehaviour
{
    Image image;
    public byte r = 0;
    public byte g = 0;
    public byte b = 0;
    public byte rOld = 0;
    public byte gOld = 0;
    public byte bOld = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(r!=rOld || g!=gOld || b != bOld)
        {
            image.color = new Color32(r, g, b, 255);
            rOld = r;
            gOld = g;
            bOld = b;
        }
    }

    public void ChangeColor(byte r, byte g ,byte b , byte a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
