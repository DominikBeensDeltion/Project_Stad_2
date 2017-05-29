using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{

    private GameObject player;

    public List<GameObject> pickups = new List<GameObject>();
    public int allowedPickups;
    public static int currentPickups;
    public float xRad;
    public float zRad;
    public Vector3 pickupSpawnLoc;

    public float speedToGive;
    public float moveSpeedDuration;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        GeneratePickups();
    }

    public void GeneratePickups()
    {
        if (currentPickups < allowedPickups)
        {
            pickupSpawnLoc = GenerateSpawn();

            int i = Random.Range(0, pickups.Count);

            RaycastHit hit;
            if (Physics.Raycast(pickupSpawnLoc, Vector3.down, out hit))
            {
                if (hit.transform.tag == "Road")
                {
                    Instantiate(pickups[i], pickupSpawnLoc, Quaternion.identity);
                    currentPickups += 1;
                }
                else
                {
                    GenerateSpawn();
                    GeneratePickups();
                }
            }
        }
    }

    public Vector3 GenerateSpawn()
    {
        float rndX = Random.Range(-xRad, xRad);
        float rndZ = Random.Range(-zRad, zRad);

        Vector3 spawnPos = new Vector3(rndX, 1, rndZ);

        return spawnPos;
    }

    public IEnumerator RemoveSpeed()
    {
        yield return new WaitForSeconds(moveSpeedDuration);
        player.GetComponent<CharacterController>().moveSpeed -= speedToGive;
    }
}
