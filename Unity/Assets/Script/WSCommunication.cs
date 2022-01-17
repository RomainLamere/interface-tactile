using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Socket.Quobject.SocketIoClientDotNet.Client;

public class WSCommunication : MonoBehaviour
{
    private QSocket socket;
    private void Start()
    {
        socket = IO.Socket("http://localhost:3000");

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.Emit("fromUnity","wesh wesh");
            print("message envoyé");
        }
    }

    public void SendMessage(string message)
    {
        socket.Emit("fromUnity",message);
    }
    
}
