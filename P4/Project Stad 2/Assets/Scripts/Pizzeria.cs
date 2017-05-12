using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzeria : MonoBehaviour {
    public List<GameObject> houses = new List<GameObject>();
    public GameObject targetHouse;
    public UIManager ui;
    public GameManager gm;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
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
        targetHouse.GetComponent<House>().isTarget = true;
        targetHouse.GetComponent<House>().CreateMarker();
        ui.TempHouseText(targetHouse);
        gm.timerOn = true;
        
    }

    void findHouses()
    {
        GameObject[] homes = GameObject.FindGameObjectsWithTag("House");
        foreach(GameObject g in homes)
        {
            houses.Add(g);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(targetHouse.GetComponent<House>().isTarget == false)
            {
                ChooseHouse();
            }
        }
    }
}
