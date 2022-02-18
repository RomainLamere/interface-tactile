using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HandlePadSounds : MonoBehaviour
{
    private string fileName = "/PadSounds";
    private List<FileStream> padSounds = new List<FileStream>();
    private int count = 0;
    private int oldCount = 0;
    private byte[] data;
    // Start is called before the first frame update
    void Start()
    {
        Directory.CreateDirectory(Application.persistentDataPath + fileName);
        DestroyAllSounds();
    }

    // Update is called once per frame
    void Update()
    {
        if(oldCount != count)
        {
            oldCount = count;
            UpdateAddSound();
        }
    }

    private void DestroyAllSounds()
    {
        foreach(string fs in Directory.GetFiles(Application.persistentDataPath + fileName))
        {
            File.Delete(fs);
        }
    }

    public void AddSound(byte[] data)
    {
        count = (count % 8) + 1;
        print("count " +count);
        this.data = data;
        
    }

    public void UpdateAddSound()
    {
        FileStream fileStream = File.Create(Application.persistentDataPath + fileName + "/" + count + ".wav");
        print("New Sound");
        for (int i = 0; i < data.Length; i++)
        {
            fileStream.WriteByte(data[i]);
        }
    }
}
