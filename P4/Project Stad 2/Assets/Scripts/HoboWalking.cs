using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoboWalking : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
    private GoalManager goalManager;
    private GameManager gm;
    public Pizzabox pizzabox;

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
        goalManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GoalManager>();
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        NavMesh.pathfindingIterationsPerFrame = 7000;
        SetNewPath();
    }

    private void Update()
    {
        switch (hoboState)
        {
            case State.Wandering:
                StateWandering();
                break;
            case State.Chasing:
                StateChasing();
                break;
            case State.Backing:
                StateBacking();
                break;
        }
    }

    public void StateWandering()
    {
        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        agent.speed = 2;

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

    public void StateChasing()
    {
        anim.SetBool("Running", true);

        if (chasePlayer)
        {
            agent.speed = 4.5f;
            agent.SetDestination(player.transform.position);
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (PizzaQuality.quality > 0)
            {
                StartCoroutine(AttackCldw());
            }
        }

        if (agent.remainingDistance >= stopChasingDistance || Pizzeria.playerInsidePizzeria || !gm.onMission)
        {
            hoboState = State.Backing;
        }
    }

    public void StateBacking()
    {
        anim.SetBool("Walking", true);
        anim.SetBool("Running", false);

        //print("hobo walking back");

        agent.speed = 2;

        agent.SetDestination(spottedPlayerPos);
        chasePlayer = false;

        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance)
        {
            canSetNewPath = true;
            hoboState = State.Wandering;
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
            pizzabox.GetHit();
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
                if (goalManager.goal6CurrentAmount < goalManager.goal6AmountToReach)
                {
                    goalManager.goal6CurrentAmount = goalManager.AddToCurrentAmount(goalManager.goal6CurrentAmount, goalManager.goal6AmountToReach);
                }
                spawnerISpawnedFrom.GetComponent<HoboSpawner>().currentHobos.Remove(gameObject);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if(col.gameObject.tag == "RidingHobo")
        {
                spawnerISpawnedFrom.GetComponent<HoboSpawner>().currentHobos.Remove(gameObject);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
                //Debug.Log("Hobo died");
        }
    }
}
