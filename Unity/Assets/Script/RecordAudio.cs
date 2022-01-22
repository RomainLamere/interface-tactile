using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        print("yo data "+ data[0]);
        print("yo channel "+ channels);
    }
}
