using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzeria : MonoBehaviour
{

    private GameManager gm;
    private UIManager uim;
    private GameObject player;

    public List<GameObject> houses = new List<GameObject>();
    public GameObject targetHouse;
    public GameObject pointer;
    public AudioSource beepBeep;

    public bool playerInsidePizzeria;

	void Start ()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        player = GameObject.FindWithTag("Player");

        FindHouses();
    }
	
	void Update ()
    {
        if (targetHouse != null)
        {
            Debug.Log(targetHouse.name);
        }
	}

    public void ChooseHouse()
    {
        int i = Random.Range(0, houses.Count);
        targetHouse = houses[i];
        targetHouse.GetComponent<House>().isTarget = true;
        targetHouse.GetComponent<House>().CreateMarker();
        uim.TempHouseText(targetHouse);
        gm.timerOn = true;
        gm.onMission = true;
        PizzaQuality.quality = 100F;
        beepBeep.Play();
        
    }

    void FindHouses()
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
            if (targetHouse.GetComponent<House>().isTarget == false)
            {
                ChooseHouse();
            }
        }
    }
}
