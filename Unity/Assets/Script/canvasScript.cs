using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class canvasScript : MonoBehaviour, IPointerEnterHandler
{

    private float time = 0;
    private bool isClicked = false;
    public GameObject prefabMenu;
    private Vector2 clicPosition;
    public EventSystem eventSystem;
    private PointerEventData myEventData;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        print("load");
    }

    // Update is called once per frame
    void Update()
    {

        if (isClicked && time < 1)
        {
            time += Time.deltaTime;
        }
        if (time >= 1 && isClicked)
        {
            GameObject newMenu =  Instantiate(prefabMenu,this.transform);
            newMenu.transform.position = new Vector3(clicPosition.x, clicPosition.y, 0);
            newMenu.GetComponent<DragObject>().canvas = GetComponentInParent<Canvas>();
            OnMouseUp();
            HandleMenuRotation(newMenu);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myEventData = eventData;
    }

    public void OnMouseDown()
    {
        if (eventSystem.IsPointerOverGameObject() == false || myEventData.pointerCurrentRaycast.gameObject.name == "background")
        {
            clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
            time = 0;
            isClicked = true;
        }
    }
    public void OnMouseUp()
    {
        isClicked = false;
    }

    private void HandleMenuRotation(GameObject menu)
    {
        float cameraWidth = Camera.main.scaledPixelWidth;
        float cameraHeight = Camera.main.scaledPixelHeight;
        if(clicPosition.y > 0)
        {
            menu.GetComponent<RectTransform>().Rotate(0f, 0f, 180f);
        }
        else
        {
            menu.GetComponent<RectTransform>().Rotate(0f, 0f, 0f);
        }
    }
}
