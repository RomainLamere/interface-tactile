using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordCircleScript : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public GameObject recorderImage;
    public GameObject circleOfRecord;
    public void OnClic()
    {
        if (image1.activeSelf)
        {
            image1.SetActive(false);
            image2.SetActive(true);
            recorderImage.SetActive(true);

        }
        else
        {
            image1.SetActive(true);
            image2.SetActive(false);
            recorderImage.SetActive(false);
            circleOfRecord.SetActive(true);
        }
    }
}
