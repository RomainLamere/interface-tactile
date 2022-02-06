using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation
{
    public int touchId;
    public float timer;
    public Vector2 position;


    public touchLocation(int newTouchId, Vector2 position)
    {
        touchId = newTouchId;
        timer = 0;
        this.position = position;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}