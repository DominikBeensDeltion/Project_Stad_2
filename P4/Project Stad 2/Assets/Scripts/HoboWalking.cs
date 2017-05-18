using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoboWalking : MonoBehaviour
{

    public float randomX;
    public float randomZ;
    public Vector3 moveToLocation;

    public NavMeshAgent agent;
    public float moveSpeed = 5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewPath();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 1.0f)
        {
            SetNewPath();
        }
    }

    public void SetNewPath()
    {
        moveToLocation = CreateWaypoint();
        agent.SetDestination(moveToLocation);
    }

    public Vector3 CreateWaypoint()
    {
        randomX = Random.Range(transform.position.x - 10, transform.position.x + 10);
        randomZ = Random.Range(transform.position.z - 10, transform.position.z + 10);

        Vector3 nextWayPoint = new Vector3(randomX, transform.position.y, randomZ);

        return nextWayPoint;
    }
}
