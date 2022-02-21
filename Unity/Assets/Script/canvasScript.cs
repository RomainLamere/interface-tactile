using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static touchLocation;


public class canvasScript : MonoBehaviour, IPointerEnterHandler
{
    public GameObject prefabMenu;
    private Vector2 clicPosition;
    public EventSystem eventSystem;
    private PointerEventData myEventData;
    public List<touchLocation> touches = new List<touchLocation>();
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        print("load");
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;


            //print(myEventData.pointerCurrentRaycast.gameObject.name);

        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("touch began");

                touches.Add(new touchLocation(t.fingerId, t.position, myEventData.pointerCurrentRaycast.gameObject.name));
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            else
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                if (thisTouch != null)
                {
                    thisTouch.UpdateTimer();
                    if (thisTouch.timer > 1 && (thisTouch.name == "background" || thisTouch.name == "A"|| thisTouch.name == "B"|| thisTouch.name == "C"|| thisTouch.name == "D") && GameObject.FindGameObjectsWithTag("menu").Length <4)
                    {
                        GameObject newMenu = Instantiate(prefabMenu, this.transform);
                        newMenu.GetComponent<RectTransform>().localPosition = getTouchPosition(thisTouch.position);
                        newMenu.GetComponent<DragObject>().canvas = GetComponentInParent<Canvas>();
                        clicPosition = thisTouch.position;
                        touches.RemoveAt(touches.IndexOf(thisTouch));
                    }
                }
            }
            ++i;

        }
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return new Vector3(touchPosition.x-1920/2, touchPosition.y-1080/2, transform.position.z);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        myEventData = eventData;
    }

    public void OnMouseDown()
    {

    }
}
