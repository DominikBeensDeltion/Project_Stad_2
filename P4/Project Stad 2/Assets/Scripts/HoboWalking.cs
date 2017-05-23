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
    public bool chasing = false;
    public bool chasePlayer = false;

    public float continueToWalkChance = 0.66f;
    public float minimumStopTime = 4f;
    public float maximumStopTime = 10f;

    public GameObject sight;
    public Vector3 spottedPlayerPos;
    public float stopChasingDistance = 25f;

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
                float stopOrNah = Random.value;

                if (stopOrNah > continueToWalkChance)
                {
                    wandering = false;
                    StartCoroutine(StandStill());
                }
                else
                {
                    SetNewPath();
                }
            }
        }
        else if (chasing)
        {
            if (chasePlayer)
            {
                agent.destination = player.transform.position;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //reduce pizza quality, make hobo slow down and start chasing again
            }

            if (Vector3.Distance(agent.transform.position, player.transform.position) >= stopChasingDistance)
            {
                agent.SetDestination(spottedPlayerPos);
                chasePlayer = false;
            }

            if (agent.transform.position == spottedPlayerPos && chasePlayer == false)
            {
                wandering = true;
                chasing = false;
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

    public IEnumerator StandStill()
    {
        yield return new WaitForSeconds(Random.Range(minimumStopTime, maximumStopTime));
        wandering = true;
        SetNewPath();
    }

    public void ChasePlayer()
    {
        chasing = true;
        spottedPlayerPos = transform.position;
        wandering = false;

        chasePlayer = true;
    }

    public Vector3 CreateWaypoint()
    {
        randomX = Random.Range(transform.position.x - newLocationRadius, transform.position.x + newLocationRadius);
        randomZ = Random.Range(transform.position.z - newLocationRadius, transform.position.z + newLocationRadius);

        Vector3 nextWayPoint = new Vector3(randomX, transform.position.y, randomZ);

        return nextWayPoint;
    }
}
