using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WSCommunication : MonoBehaviour
{
    WebSocket ws;
    private void Start()
    {
        ws = new WebSocket("ws://09d8-134-59-215-253.ngrok.io/");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            print(sender);
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        
    }
    private void Update()
    {
        if (ws == null)
        {
            return ;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("ws://7d42-134-59-215-253.ngrok.io/");
            print("message envoyé");
        }
    }

    public void SendMessage(string message)
    {
        ws.Send(message);
    }
    
}
