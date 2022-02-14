using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayTheDrum : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public AudioSource audioSource;
    public WSCommunication wSCommunication;
    public Image image;
    public Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        wSCommunication = FindObjectOfType<WSCommunication>();
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        print("yo yo yo");
        audioSource.Play();
        this.transform.localScale = scale * 1.2f;
        wSCommunication.SendMessage(this.name);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = scale;
    }


}
