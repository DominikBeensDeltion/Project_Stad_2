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
            int i = Random.Range(0, pickups.Count);
            float rndX = Random.Range(0, xRad);
            float rndZ = Random.Range(0, zRad);
            Vector3 spawnPos = new Vector3(rndX, 1, rndZ);
            RaycastHit hit;
            if (Physics.Raycast(spawnPos, Vector3.down, out hit))
            {
                if (hit.transform.tag == "Road")
                {
                    Instantiate(pickups[i], hit.transform.position, Quaternion.identity);
                    currentPickups += 1;
                }
                else
                {
                    GeneratePickups();
                }
            }
        }
    }

    public IEnumerator RemoveSpeed()
    {
        yield return new WaitForSeconds(moveSpeedDuration);
        player.GetComponent<CharacterController>().moveSpeed -= speedToGive;
    }
}
