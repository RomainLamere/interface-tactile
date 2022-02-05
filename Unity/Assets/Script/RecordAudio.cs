using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // for FileStream
using System; // for BitConverter and Byte Type

public class RecordAudio : MonoBehaviour
{
    private int bufferSize;
    private int numBuffers;
    private FileStream fileStream;
    private string fileName = "/recTest.wav";
    private int outputRate = 44100;
    private int headerSize = 44;
    private bool recOutput;
    //private string path = "Assets/Sounds";
    private bool record = false;
    private WSCommunication wSCommunication;
    private String zone;
    // Start is called before the first frame update
    void Start()
    {
        AudioSettings.outputSampleRate = outputRate;
        wSCommunication = FindObjectOfType<WSCommunication>();
    }
    public void SetZone(string zone)
    {
        print("my zone is " + zone);
        this.zone = zone;
    }
    // Update is called once per frame
    void Awake()
    {
        AudioSettings.GetDSPBufferSize(out bufferSize,out numBuffers);
    }

    void Update()
    {
        if (record)
        {
            record = false;
            print("rec");
            if (recOutput == false)
            {
                print("rec start");
                StartWriting(fileName);
                recOutput = true;
            }
            else
            {
                recOutput = false;
                WriteHeader();
                print("rec stop");
                SendRecord();
            }
        }
    }
    public void SendRecord()
    {
        wSCommunication.SendRecord(fileStream,zone);
    }
    public void Record()
    {
        if (record==false) record = true;
    }

    public void StartWriting(string name)
    {
        print(Application.persistentDataPath);
        fileStream = new FileStream(Application.persistentDataPath + name, FileMode.Create);
        //fileStream = File.Create(path);
        byte emptyByte = new byte();

        for (int i = 0; i < headerSize; i++) //preparing the header
    {
            fileStream.WriteByte(emptyByte);
        }
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (recOutput)
        {
           
            short[] intData = new short[data.Length];
            //converting in 2 steps : float[] to Int16[], //then Int16[] to Byte[]

            byte[] bytesData = new byte[data.Length * 2];
            //bytesData array is twice the size of
            //dataSource array because a float converted in Int16 is 2 bytes.

            int rescaleFactor = 32767; //to convert float to Int16

            for (int i = 0; i < data.Length; i++)
            {
                intData[i] = (short)(data[i] * rescaleFactor);
                byte[] byteArr = new byte[2];
                byteArr = BitConverter.GetBytes(intData[i]);
                byteArr.CopyTo(bytesData, i * 2);
            }

            fileStream.Write(bytesData, 0, bytesData.Length);
        }
    }
    void WriteHeader()
    {

        fileStream.Seek(0, SeekOrigin.Begin);

        byte[] riff= System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        ushort two  = 2;
        ushort one  = 1;

        byte[] audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        byte[] numChannels = BitConverter.GetBytes(two);
        fileStream.Write(numChannels, 0, 2);

        byte[] sampleRate = BitConverter.GetBytes(outputRate);
        fileStream.Write(sampleRate, 0, 4);

        byte[] byteRate = BitConverter.GetBytes(outputRate * 4);
        // sampleRate * bytesPerSample*number of channels, here 44100*2*2

        fileStream.Write(byteRate, 0, 4);

        ushort four = 4;
        byte[] blockAlign = BitConverter.GetBytes(four);
        fileStream.Write(blockAlign, 0, 2);

        ushort sixteen = 16;
        byte[] bitsPerSample = BitConverter.GetBytes(sixteen);
        fileStream.Write(bitsPerSample, 0, 2);

        byte[] dataString = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(dataString, 0, 4);

        byte[] subChunk2 = BitConverter.GetBytes(fileStream.Length - headerSize);
        fileStream.Write(subChunk2, 0, 4);

        fileStream.Close();
    }
}
