using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordCirlce : MonoBehaviour
{
    private bool record = false;
    private GameObject myPiano;
    private RecordAudio recordAudio;
    // Start is called before the first frame update
    void Start()
    {
        recordAudio = FindObjectOfType<RecordAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Distance(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2));
    }
    public void OnClic()
    {
        if (record)
        {
            StopRecord(myPiano);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            record = false;
        }
        else
        {
            record = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            Vector2 pos1 = transform.parent.transform.parent.GetComponent<RectTransform>().localPosition;
            GameObject[] go = GameObject.FindGameObjectsWithTag("piano");
            myPiano = go[0];
            foreach (GameObject o in go)
            {
                if (Distance(pos1, o.GetComponent<RectTransform>().localPosition) < Distance(pos1, myPiano.GetComponent<RectTransform>().localPosition))
                {
                    myPiano = o;
                }
            }

            StartRecord(myPiano);
        }
    }
    private void StartRecord(GameObject go)
    {
        recordAudio.SetZone(myPiano.GetComponent<HandlePiano>().spotName);
        recordAudio.Record();
        go.transform.GetChild(1).gameObject.SetActive(true);
    }
    private void StopRecord(GameObject go)
    {
        recordAudio.Record();
        go.transform.GetChild(1).gameObject.SetActive(false);
    }
}
