using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMovement : MonoBehaviour
{
    private GameObject endPoint;
    private NavMeshAgent agent;
    [SerializeField] private int checkpointCounter;
    [SerializeField] private GameObject[] myEndPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        checkpointCounter = 1;
        endPoint = GameObject.Find("EndPoint 1");
        myEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");

        if (myEndPoints.Length <= 0)
        {
            agent.isStopped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.isStopped)
        {
            agent.SetDestination(endPoint.transform.position);
        }  
    }
    IEnumerator NextTarget(int targetNumber)
    {     
 
        yield return new WaitForSeconds(2);
        endPoint = GameObject.Find("EndPoint " + targetNumber);
        myEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        agent.isStopped = false;
        if (myEndPoints.Length <= 0)
        {
            agent.isStopped = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndPoint"))
        {
            agent.isStopped = true;
            checkpointCounter++;
            StartCoroutine(NextTarget(checkpointCounter));

            Destroy(other.gameObject);
        }
    }
}
