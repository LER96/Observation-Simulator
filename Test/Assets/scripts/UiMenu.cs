using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenu : MonoBehaviour
{
    //Header is a way to orginize the inspector meaning you add a Title to a bunch of public to inspector objects
    [Header("Cameras")]
    [SerializeField] private GameObject ramaBlue;
    [SerializeField] private GameObject blueWave;
    private void Awake()
    {
        //Activate 1 camera to show something
        ramaBlue.SetActive(true);
        blueWave.SetActive(false);

        //Stop the time
        Time.timeScale = 0;
    }

    public void ButtonPress(string camera)
    {
        if(camera == "RamaBlue")
        {
            //Activate RamaBlue Camera and disable the other camera and the UI
            ramaBlue.SetActive(true);
            blueWave.SetActive(false);
            gameObject.SetActive(false);

            //Resume the time
            Time.timeScale = 1;

        }
        else if (camera == "BlueWave")
        {
            //Activate BlueWave Camera and disable the other camera and the UI
            ramaBlue.SetActive(false);
            blueWave.SetActive(true);
            gameObject.SetActive(false);

            //Resume the time
            Time.timeScale = 1;
        }
    }
}
