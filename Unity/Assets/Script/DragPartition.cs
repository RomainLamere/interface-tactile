using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPartition : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
