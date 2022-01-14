using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTheKey : MonoBehaviour
{
    public AudioSource audioSource;
    public WSCommunication wSCommunication;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        audioSource.Play();
        wSCommunication.SendMessage(this.name);
    }
}
