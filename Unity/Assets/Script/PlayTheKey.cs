using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayTheKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource audioSource;
    public WSCommunication wSCommunication;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        wSCommunication = FindObjectOfType<WSCommunication>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.Play();
        if(transform.parent.name=="Blanches") image.color = new Color(0.5f, 0.5f, 0.5f, 1);
        else image.color = new Color(0.25f, 0.25f, 0.25f, 1);
        wSCommunication.SendMessage(this.name);

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (transform.parent.name == "Blanches") image.color = new Color(1f, 1f, 1f, 1);
        else image.color = new Color(0f, 0f, 0f, 1);
    }


}
