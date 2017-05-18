using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoboWalking : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;

    private float randomX;
    private float randomZ;
    public float newLocationRadius = 10f;
    public Vector3 moveToLocation;

    public bool wandering = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
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
        randomX = Random.Range(transform.position.x - newLocationRadius, transform.position.x + newLocationRadius);
        randomZ = Random.Range(transform.position.z - newLocationRadius, transform.position.z + newLocationRadius);

        Vector3 nextWayPoint = new Vector3(randomX, transform.position.y, randomZ);

        return nextWayPoint;
    }
}
