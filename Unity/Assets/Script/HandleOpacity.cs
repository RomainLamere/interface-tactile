using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleOpacity : MonoBehaviour
{
    private float fadeStart=0;
    private float fadeTime =1f;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = this.GetComponent<Image>().color;

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStart < fadeTime)
        {
            fadeStart += Time.deltaTime;

            this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.5f*(fadeStart / fadeTime));
        }
        else if (fadeStart < 2 * fadeTime)
        {
            fadeStart += Time.deltaTime;
            this.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.5f*(1-(fadeStart/2)));
        }
        else
        {
            Restart();
        }
    }

    public void Restart()
    {
        fadeStart = 0;
        gameObject.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(-3f, 3f), Random.Range(-1f, 1f));
    }
}
