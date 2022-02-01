using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System.IO; // for FileStream



public class WSCommunication : MonoBehaviour
{
    public SocketIOUnity socket;
    public Image frame;
    private void Start()
    {
        var uri = new Uri("http://192.168.43.60:3000");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        }
            );
        socket.JsonSerializer = new NewtonsoftJsonSerializer();
        print("connect");
        socket.Connect();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.Emit("fromUnity", "wesh wesh");
            print("message envoyé");
        }

        socket.On("newLights", ChangeFrameColor);
    }

    private void ChangeFrameColor(SocketIOResponse obj)
    {
        print(obj.GetValue());
    }

    public void SendMessage(string message)
    {
        socket.Emit("fromUnity", message);
        print(message);
    }

    public void SendRecord(FileStream file, string zone)
    {
        print("Send Record");
        string str = "sendRecord" + zone;

        socket.Emit(str,new RecordObject("piano",File.ReadAllBytes(file.Name)));
    }



}
[Serializable]
public class RecordObject
{
    public string instrument = "";
    public byte[] record = null;
    
    public RecordObject(string instrument, byte[] record)
    {
        this.instrument = instrument;
        this.record = record;
    }
}
