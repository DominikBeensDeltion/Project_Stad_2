using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoboWalking : MonoBehaviour
{

    private float randomX;
    private float randomZ;
    public float locationRadius = 10f;
    public Vector3 moveToLocation;
    public bool wandering = true;

    private NavMeshAgent agent;
    public float moveSpeed = 5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewPath();
    }

    private void Update()
    {
        if (wandering)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 1.0f)
            {
                SetNewPath();
            }
        }
    }

    public void SetNewPath()
    {
        moveToLocation = CreateWaypoint();

        RaycastHit hit;
        if (Physics.Raycast(moveToLocation, Vector3.down, out hit, 2))
        {
            if (hit.transform.tag == "Road")
            {
                CreateWaypoint();
                SetNewPath();
            }
            else
            {
                agent.SetDestination(moveToLocation);
            }
        }
    }

    public Vector3 CreateWaypoint()
    {
        randomX = Random.Range(transform.position.x - locationRadius, transform.position.x + locationRadius);
        randomZ = Random.Range(transform.position.z - locationRadius, transform.position.z + locationRadius);

        Vector3 nextWayPoint = new Vector3(randomX, transform.position.y, randomZ);

        return nextWayPoint;
    }
}
