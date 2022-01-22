using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class canvasScript : MonoBehaviour
{

    private float time = 0;
    private bool isClicked = false;
    public GameObject prefabMenu;
    private Vector2 clicPosition;
    // Start is called before the first frame update
    void Start()
    {
        print("load");
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked && time < 1)
        {
            print(time);
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

    public void OnMouseDown()
    {
        clicPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
        time = 0;
        isClicked = true;
    }
    public void OnMouseUp()
    {
        print("ya");
        isClicked = false;
    }

    private void HandleMenuRotation(GameObject menu)
    {
        float cameraWidth = Camera.main.scaledPixelWidth;
        float cameraHeight = Camera.main.scaledPixelHeight;
        print("clic position x" + clicPosition.x);
        if(clicPosition.x > 0)
        {
            menu.GetComponent<RectTransform>().Rotate(0f, 0f, 90f);
        }
        else
        {
            menu.GetComponent<RectTransform>().Rotate(0f, 0f, -90f);
        }
    }
}
