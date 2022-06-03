using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPicker : MonoBehaviour
{
    [SerializeField] private GameObject tankGameObject;
    [SerializeField] private GameObject thisButton;

    [Header("Roads")]
    [SerializeField] private GameObject blueRoad;
    [SerializeField] private GameObject greenRoad;
/*    [SerializeField] private GameObject yellowRoad;
    [SerializeField] private GameObject redRoad;*/
    private void Awake()
    {
        tankGameObject.SetActive(false);
    }
    public void PickPath()
    {
        int randomNum = Random.Range(1, 5);
        if(randomNum == 1)
        {
            blueRoad.SetActive(true);
            tankGameObject.SetActive(true);
            thisButton.SetActive(false);
        }
        else if (randomNum == 2)
        {
            greenRoad.SetActive(true);
            tankGameObject.SetActive(true);
            thisButton.SetActive(false);
        }
/*        else if (randomNum == 3)
        {
            yellowRoad.SetActive(true);
            tankGameObject.SetActive(true);
            thisButton.SetActive(false);

        }
        else if (randomNum == 4)
        {
            redRoad.SetActive(true);
            tankGameObject.SetActive(true);
            thisButton.SetActive(false);
        }
*/
    }

}
