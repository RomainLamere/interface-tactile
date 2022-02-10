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
    public HandleColor frame;
    private void Start()
    {
        var uri = new Uri("http://Localhost:3000");
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

        socket.On("newLights", ChangeFrameColor);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.Emit("fromUnity", "wesh wesh");
            print("message envoy�");
        }

        
    }

    public void ChangeFrameColor(SocketIOResponse obj)
    {
        byte r = byte.Parse(obj.GetValue().ToString().Substring(1,2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(obj.GetValue().ToString().Substring(3,2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(obj.GetValue().ToString().Substring(5,2), System.Globalization.NumberStyles.HexNumber);
        
        print("new color :\nr = " + r + "/ g = " + g  + "/ b = " + b );
        frame.ChangeColor(r, g, b, 255);
    }

    public void SendMessage(string message)
    {
        socket.Emit("fromUnity", message);
        print(message);
    }

    public void SendRecord(FileStream file, string zone)
    {
        
        string str = "sendRecord" + zone;
        print(zone);
        print(str);
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
