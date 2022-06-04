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
    [SerializeField] private GameObject yellowRoad;
    [SerializeField] private GameObject redRoad;
    private void Awake()
    {
        //Set all of the game objects to Off mode untill the user starts the following practice
        tankGameObject.SetActive(false);
        blueRoad.SetActive(false);
        greenRoad.SetActive(false);
        yellowRoad.SetActive(false);
        redRoad.SetActive(false);
    }
    public void ButtonUsage(string buttonName)
    {
        //Check to see what button is pressed
        if (buttonName == "Road Picker")
        {
            //Set an Int as a random number to choose which road to take
            int randomNum = Random.Range(1,5);
            if (randomNum == 1)
            {
                //The tank will use the blue road 
                blueRoad.SetActive(true);
                tankGameObject.SetActive(true);

                //Disable the Button to spawn another road
                thisButton.SetActive(false);
                print("Blue Road");
            }
            else if (randomNum == 2)
            {
                //The tank will use the green road 
                greenRoad.SetActive(true);
                tankGameObject.SetActive(true);

                //Disable the Button to spawn another road
                thisButton.SetActive(false);
                print("Green Road");
            }
            else if (randomNum == 3)
            {
                //The tank will use the yellow road 
                yellowRoad.SetActive(true);
                tankGameObject.SetActive(true);

                //Disable the Button to spawn another road
                thisButton.SetActive(false);
                print("Yellow Road");

            }
            else if (randomNum == 4)
            {
                //The tank will use the red road 
                redRoad.SetActive(true);
                tankGameObject.SetActive(true);

                //Disable the Button to spawn another road
                thisButton.SetActive(false);
                print("Red Road");
            }
            else
            {
                //A failsafe incase there is a number that isn't on the list
                //in that case starts the procedure again (until it finds a number)
                print(randomNum + " WTF IS THIS NUMBER???");
                ButtonUsage("Road Picker");
            }
        }
        else if (buttonName == "Quit Simulator")
        {
            //Close the game if the user choose the quit simulator button
            Application.Quit();
            print("Quitting Game...");
        }
    }


}
