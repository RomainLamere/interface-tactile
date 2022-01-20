using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler
{
    RectTransform rectTransform;
    [SerializeField] public Canvas canvas;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvas.GetComponent<canvasScript>().OnMouseUp();
    }
}
