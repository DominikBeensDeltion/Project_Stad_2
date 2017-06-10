﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboSpawner : MonoBehaviour
{

    public bool canSpawn = true;
    public bool ableToSpawn;
    public int maxSpawnAmount = 5;
    public int currentSpawnAmount;
    public float spawnInterval = 1;

    public List<GameObject> currentHobos = new List<GameObject>();
    public GameObject hoboPrefab;
    public GameObject hobo;

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
            StartCoroutine(SpawnHobo());
        }
    }

    public IEnumerator SpawnHobo()
    {
        canSpawn = false;
        hobo = Instantiate(hoboPrefab, transform.position, Quaternion.identity);
        hobo.GetComponent<HoboWalking>().spawnerISpawnedFrom = gameObject;
        currentHobos.Add(hobo);
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }
}
