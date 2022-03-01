using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
public class UpdateTouches : MonoBehaviour
{
    public string soundPath;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(UpdateAllTouche());
    }
    private void Awake()
    {
        soundPath = Application.persistentDataPath + "/PadSounds/";
        StartCoroutine(UpdateAllTouche());
        count = Directory.GetFiles(Application.persistentDataPath + "/PadSounds").Length;
        print("awake");
    }

    // Update is called once per frame
    void Update()
    {
        if(count!= Directory.GetFiles(Application.persistentDataPath + "/PadSounds").Length)
        {
            StartCoroutine(wait(1f));    
        }
    }

    public IEnumerator UpdateAllTouche()
    {
        for(int i =0; i< Directory.GetFiles(Application.persistentDataPath + "/PadSounds").Length; i++)
        {
            this.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.yellow;
            WWW www = GetAudioFromFile(soundPath , i+1 + ".wav");
            yield return www;
            this.transform.GetChild(i).GetChild(0).GetComponent<AudioSource>().clip = www.GetAudioClip();
            this.transform.GetChild(i).GetChild(0).GetComponent<AudioSource>().clip.name = i+1+".wav";

        }
    }
    public IEnumerator wait(float t)
    {
        yield return new WaitForSeconds(t);
        Awake();
    }
    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }


}
