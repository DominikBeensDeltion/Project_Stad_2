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
                uim.noticeText.text = "Press E to pick up pizza";
                uim.noticeAnimator.SetTrigger("SetActive");
                uim.panelIsActive = true;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (targetHouse.GetComponent<House>().isTarget == false)
            {
                if (Input.GetButtonDown("e"))
                {
                    ChooseHouse();
                    StartCoroutine(PizzaPickupNotice());
                    uim.noticeAnimator.SetTrigger("SetInActive");
                    uim.panelIsActive = false;
                }
            }
            else if (targetHouse.GetComponent<House>().isTarget == true)
            {
                if (Input.GetButtonDown("e"))
                {
                    if (!uim.panelIsActive)
                    {
                        uim.noticeText.text = "You are already carrying a pizza!";
                        uim.noticeAnimator.SetTrigger("SetActive");
                        uim.panelIsActive = true;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uim.noticeAnimator.SetTrigger("SetInActive");
            uim.panelIsActive = false;
        }
    }

    public IEnumerator PizzaPickupNotice()
    {
        uim.orderText.text = "Picked up pizza!" + "\n\n" + "Now get delivering!";
        uim.orderAnimator.SetBool("Order", true);

        yield return new WaitForSeconds(3);

        uim.orderAnimator.SetBool("Order", false);
    }
}
