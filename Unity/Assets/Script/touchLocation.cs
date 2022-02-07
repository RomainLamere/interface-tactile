using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation
{
    public int touchId;
    public float timer;
    public Vector2 position;
    public string name;


    public touchLocation(int newTouchId, Vector2 position,string name)
    {
        touchId = newTouchId;
        timer = 0;
        this.position = position;
        this.name = name;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}