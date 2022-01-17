using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;



public class WSCommunication : MonoBehaviour
{
    public SocketIOUnity socket;

    private void Start()
    {
        var uri = new Uri("http://localhost:3000");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        }
            );
        socket.JsonSerializer = new NewtonsoftJsonSerializer();
        socket.Connect();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.Emit("fromUnity", "wesh wesh");
            print("message envoyé");
        }
    }

    public void SendMessage(string message)
    {
        socket.Emit("fromUnity", message);
        print(message);
    }

}
