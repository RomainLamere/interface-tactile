using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTheKey : MonoBehaviour
{
    public AudioSource audioSource;
    public WSCommunication wSCommunication;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseEnter()
    {
        audioSource.Play();
        image.color = new Color(0.5f, 0.5f, 0.5f, 1);

    }
    public void OnMouseExit()
    {
        image.color = new Color(1f, 1f, 1f, 1);
    }

    public void OnMouseDown()
    {
        wSCommunication.SendMessage(name);
    }
}
