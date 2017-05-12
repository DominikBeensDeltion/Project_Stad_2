using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzeria : MonoBehaviour {
    public List<GameObject> houses = new List<GameObject>();
    public GameObject targetHouse;
	// Use this for initialization
	void Start () {
        findHouses();
        ChooseHouse();

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(targetHouse.name);
	}

    public void ChooseHouse()
    {
        int i = Random.Range(0, houses.Count);
        targetHouse = houses[i];
    }

    void findHouses()
    {
        GameObject[] homes = GameObject.FindGameObjectsWithTag("House");
        foreach(GameObject g in homes)
        {
            houses.Add(g);
        }
    }
}
