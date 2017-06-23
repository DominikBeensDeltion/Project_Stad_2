using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject pizzacar;

    public bool canSpawn;
    public bool ableToSpawn;
    public int maxSpawnAmount = 5;
    public int currentSpawnAmount;
    public float spawnInterval = 1;
    public float canSpawnDistance = 25f;

    public List<GameObject> currentHobos = new List<GameObject>();
    public List<Material> materials = new List<Material>();
    public GameObject hoboPrefab;
    private GameObject hobo;

    private void Update()
    {
        currentSpawnAmount = currentHobos.Count;

        if (currentSpawnAmount < maxSpawnAmount)
        {
            ableToSpawn = true;
        }
        else
        {
            ableToSpawn = false;
        }

        if (ableToSpawn && canSpawn)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= canSpawnDistance || Vector3.Distance(transform.position, pizzacar.transform.position) >= canSpawnDistance)
            {
                StartCoroutine(SpawnHobo());
            }
        }
    }

    public IEnumerator SpawnHobo()
    {
        canSpawn = false;
        hobo = Instantiate(hoboPrefab, transform.position, Quaternion.identity);
        hobo.GetComponent<HoboWalking>().spawnerISpawnedFrom = gameObject;
        hobo.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = materials[Random.Range(0, materials.Count)];
        currentHobos.Add(hobo);
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCar")
        {
            canSpawn = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCar")
        {
            canSpawn = false;

            for (int i = 0; i < currentHobos.Count; i++)
            {
                if (currentHobos[i].GetComponent<HoboWalking>().hoboState != HoboWalking.State.Wandering)
                {
                    currentHobos[i].GetComponent<HoboWalking>().selfDestroyWhenStoppedBacking = true;
                    currentHobos.Remove(currentHobos[i]);
                }
                else
                {
                    Destroy(currentHobos[i].gameObject);
                    currentHobos.Remove(currentHobos[i]);
                }
            }
        }
    }
}
