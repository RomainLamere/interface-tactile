using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayThePad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource audioSource;
    public WSCommunication wSCommunication;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = transform.parent.GetComponent<Image>();
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
        image.color = Color.cyan;
        wSCommunication.SendMessage(this.name);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = Color.white;
    }


}

