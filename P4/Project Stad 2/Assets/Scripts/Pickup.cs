using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public GameManager gm;
    public float timeToGive;
    private float randomX;
    private float randomZ;
    public float newLocationRadius = 10f;
    public Vector3 pickupSpawn;
    public int allowedPickups;
    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
        Calculate();
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerCar")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        gm.timeToCountDown += timeToGive;
        //Calculate();
    }

    public void SpawnPickup()
    {
        pickupSpawn = FindLocation();

        RaycastHit hit;
        if (Physics.Raycast(pickupSpawn, Vector3.down, out hit, 2))
        {
            if (hit.transform.tag == "Road" && hit.transform.tag != "Pickup")
            {
              Vector3 spwnLoc = new Vector3(hit.transform.position.x, 1, hit.transform.position.z);
                Instantiate(gameObject, spwnLoc, Quaternion.identity);
            }
            else
            {
                FindLocation();
                SpawnPickup();
            }
        }
    }

    public Vector3 FindLocation()
    {
        randomX = Random.Range(transform.position.x - newLocationRadius, transform.position.x + newLocationRadius);
        randomZ = Random.Range(transform.position.z - newLocationRadius, transform.position.z + newLocationRadius);

        Vector3 nextWayPoint = new Vector3(randomX, transform.position.y, randomZ);

        return nextWayPoint;
    }

    void Calculate()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        if (pickups.Length < allowedPickups)
        {
            SpawnPickup();
        }

    }
}
