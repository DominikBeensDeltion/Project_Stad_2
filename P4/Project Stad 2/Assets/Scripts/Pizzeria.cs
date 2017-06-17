using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzeria : MonoBehaviour
{

    private GameManager gm;
    private UIManager uim;
    public Pizzabox pizzabox;

    public List<GameObject> houses = new List<GameObject>();
    public GameObject targetHouse;
    public AudioSource beepBeep;

    public static bool playerInsidePizzeria;

    public bool canSpawnParticle;
    public bool particleSpawned;
    public Transform pizzeriaParticleSpawn;
    public GameObject particlePrefab;
    public GameObject particle;

	void Start ()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();

        FindHouses();
    }
	
	void Update ()
    {
        if (targetHouse != null)
        {
            Debug.Log(targetHouse.name);
        }

        if (!particleSpawned && canSpawnParticle)
        {
            particle = Instantiate(particlePrefab, pizzeriaParticleSpawn);
            particleSpawned = true;
        }
    }

    public void ChooseHouse()
    {
        int i = Random.Range(0, houses.Count);
        targetHouse = houses[i];
        targetHouse.GetComponent<House>().isTarget = true;
        targetHouse.GetComponent<House>().CreateMarker();
        uim.HouseText(targetHouse);
        gm.timerOn = true;
        gm.onMission = true;
        PizzaQuality.quality = 100F;
        beepBeep.Play();
        StartCoroutine(pizzabox.Active());

        Destroy(particle);
        particle = null;
        particleSpawned = false;
        canSpawnParticle = false;
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
                uim.noticePanelIsActive = true;
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
                    uim.noticePanelIsActive = false;
                }
            }
            else if (targetHouse.GetComponent<House>().isTarget == true)
            {
                if (Input.GetButtonDown("e"))
                {
                    if (!uim.noticePanelIsActive)
                    {
                        uim.noticeText.text = "You are already carrying a pizza!";
                        uim.noticeAnimator.SetTrigger("SetActive");
                        uim.noticePanelIsActive = true;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (uim.noticePanelIsActive)
            {
                uim.noticeAnimator.SetTrigger("SetInActive");
                uim.noticePanelIsActive = false;
            }
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
