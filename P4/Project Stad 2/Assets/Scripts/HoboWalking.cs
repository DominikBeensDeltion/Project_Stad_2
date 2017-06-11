using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoboWalking : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;

    public enum State
    {
        Wandering,
        Chasing,
        Backing
    }
    public State hoboState;

    private float randomX;
    private float randomZ;
    public float newLocationRadius = 10f;
    public Vector3 moveToLocation;

    public bool canSetNewPath = true;
    public bool chasePlayer;
    public bool walkingBack;

    public float continueToWalkChance = 0.66f;
    public float minimumStopTime = 4f;
    public float maximumStopTime = 10f;

    public Vector3 spottedPlayerPos;
    public float stopChasingDistance = 25f;

    public float pizzaDamage = 30F;
    public bool attackCool;
    public float attackSpeed;

    public AudioSource nom;
    public GameObject deathParticle;

    public GameObject spawnerISpawnedFrom;

    public Animator anim;

    private void Start()
    {
        hoboState = State.Wandering;

        nom = gameObject.GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        SetNewPath();
    }

    private void Update()
    {
        if (hoboState == State.Wandering)
        {
            if (agent.velocity != Vector3.zero)
            {
                //anim.SetBool("Walking", true);
            }
            else
            {
                //anim.SetBool("Walking", false);
            }

            if (canSetNewPath)
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    float stopOrNah = Random.value;

                    if (stopOrNah > continueToWalkChance)
                    {
                        canSetNewPath = false;
                        StartCoroutine(StandStill());
                    }
                    else
                    {
                        SetNewPath();
                    }
                }
            }
        }
        else if (hoboState == State.Chasing)
        {
            //anim.SetBool("Running", true);
            //anim.SetBool("Walking", false);

            if (chasePlayer)
            {
                agent.SetDestination(player.transform.position);
            }

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (PizzaQuality.quality > 0)
                {
                    StartCoroutine(AttackCldw());
                }
            }

            if (agent.remainingDistance >= stopChasingDistance || Pizzeria.playerInsidePizzeria)
            {
                hoboState = State.Backing;
            }
        }
        else if (hoboState == State.Backing)
        {
            //anim.SetBool("Walking", true);
            //anim.SetBool("Running", false);

            agent.SetDestination(spottedPlayerPos);
            chasePlayer = false;
            walkingBack = true;

            if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance)
            {
                canSetNewPath = true;
                walkingBack = false;

                hoboState = State.Wandering;
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
        canSetNewPath = true;
        SetNewPath();
    }

    public void ChasePlayer()
    {
        hoboState = State.Chasing;

        spottedPlayerPos = transform.position;
        canSetNewPath = false;
        chasePlayer = true;
    }

    public Vector3 CreateWaypoint()
    {
        randomX = Random.Range(transform.position.x - newLocationRadius, transform.position.x + newLocationRadius);
        randomZ = Random.Range(transform.position.z - newLocationRadius, transform.position.z + newLocationRadius);

        Vector3 nextWayPoint = new Vector3(randomX, transform.position.y, randomZ);

        return nextWayPoint;
    }

    public IEnumerator AttackCldw()
    {
        if (!attackCool)
        {
            attackCool = true;
            nom.Play();
            PizzaQuality.quality -= pizzaDamage;
            yield return new WaitForSeconds(attackSpeed);
            attackCool = false;
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PlayerCar")
        {
            if (col.gameObject.GetComponent<PizzaCar>().inCar)
            {
                spawnerISpawnedFrom.GetComponent<HoboSpawner>().currentHobos.Remove(gameObject);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
