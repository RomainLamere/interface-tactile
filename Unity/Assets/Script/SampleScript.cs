using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SampleScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject sideBar;
    // Start is called before the first frame update
    CircleCollider2D circleCollider;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.SetActive(false);
        col.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        sideBar.SetActive(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        sideBar.SetActive(false);
    }
}
