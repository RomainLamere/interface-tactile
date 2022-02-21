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
    public HandleColor frameGauche;
    public HandleColor frameMilieu;
    public HandleColor frameDroite;
    public HandlePadSounds handlePadSounds;
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
        print("connect");
        socket.Connect();
        socket.Emit("fromUnity", "Unity connection");

        socket.On("newLights", ChangeFrameColor);
        socket.On("newVoice", ReceiveSound);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.Emit("fromUnity", "wesh wesh");
            print("message envoyé");
        }
        if(!socket.Connected)
        {
            socket.Connect();
            socket.Emit("fromUnity", "Unity connection");
        }
        
    }

    public void ChangeFrameColor(SocketIOResponse obj)
    {
        print(obj);
        print(obj.GetValue().ToString().Substring(24, 1));
        byte r = byte.Parse(obj.GetValue().ToString().Substring(11,2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(obj.GetValue().ToString().Substring(13,2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(obj.GetValue().ToString().Substring(15,2), System.Globalization.NumberStyles.HexNumber);
        
        print("new color :\nr = " + r + "/ g = " + g  + "/ b = " + b );
        if(obj.GetValue().ToString().Substring(24, 1) == "1")
            frameGauche.ChangeColor(r, g, b, 255);
        else if (obj.GetValue().ToString().Substring(24, 1) == "2")
            frameMilieu.ChangeColor(r, g, b, 255);
        else
            frameDroite.ChangeColor(r, g, b, 255);
    }

    public void ReceiveSound(SocketIOResponse obj)
    {
        print("sound receive is : " + obj.GetValue().ToString());
        print("sound receive is : " + obj.InComingBytes[0]);
        handlePadSounds.AddSound(obj.InComingBytes[0]);
    }

    public void SendMessage(string message)
    {
        socket.Emit("fromUnity", message);
        print(message);
    }

    public void SendRecord(FileStream file, string zone, string instrument)
    {
        
        string str = "sendRecord" + zone;
        print(str + " instrument = " + instrument);
        socket.Emit(str,new RecordObject(instrument,File.ReadAllBytes(file.Name)));
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
