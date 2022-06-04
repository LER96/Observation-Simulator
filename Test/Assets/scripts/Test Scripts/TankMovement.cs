using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TankMovement : MonoBehaviour
{
    //The next point for the AI to follow
    private GameObject endPoint;

    //AI logic
    private NavMeshAgent agent;

    //Find the next checkpoint that the tank needs to visit
    [SerializeField] private int checkpointCounter;

    //Tracks the amount of checkpoints that tank has left to visit
    [SerializeField] private GameObject[] myEndPoints;
    
    void Start()
    {
        //Set the AI component
        agent = GetComponent<NavMeshAgent>();

        //Setting the checkpoint counter to 1
        checkpointCounter = 1;

        //Setting the first checkpoint to this game object that the AI will track
        endPoint = GameObject.Find("EndPoint 1");

        //Add all of the checkpoints to an array to track how much left
        myEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");

        //Checks to see if there is any more checkpoints left
        if (myEndPoints.Length <= 0)
        {
            //If so stop the AI at its place
            agent.isStopped = true;
        }
    }

    void Update()
    {
        //Set the destination When the agent resume to move
        if (!agent.isStopped)
        {
            agent.SetDestination(endPoint.transform.position);
        }  
    }
    IEnumerator NextTarget(int targetNumber)
    {     
 
        //Wait 2 seconds between 2 targets
        yield return new WaitForSeconds(2);

        //Sets the new target according to the checkpoint counter
        endPoint = GameObject.Find("EndPoint " + targetNumber);

        //Refresh the amount of existing checkpoints
        myEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");

        //Resume the movment of the tank
        agent.isStopped = false;

        //If there are no more checkpoints, stops the tank and restart the scene
        if (myEndPoints.Length <= 0)
        {
            agent.isStopped = true;
            SceneManager.LoadScene("Tay");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //When the tank collide with a checkpoint
        if (other.gameObject.CompareTag("EndPoint"))
        {
            //Stop the tank
            agent.isStopped = true;

            //Add to the checkpoint counter 1
            checkpointCounter++;

            //Finding the next target according to the checkpoint counter
            StartCoroutine(NextTarget(checkpointCounter));

            //Destroy the checkpoint game object
            Destroy(other.gameObject);
        }
    }
}
