using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    private UIManager uim;
    private GameManager gm;
    private GoalManager goalManager;
    private Pizzeria pizzeriaScript;
    public Pizzabox pizzabox;

    public bool isTarget;
    public GameObject markOne;
    public GameObject cloneOne;
    public string naam;

    public int pointsGive = 100;
    public int bonusPoints = 50;

    public Transform particleSpawn;
    public GameObject particlePrefab;
    public GameObject particle;

    public GameObject goalCompleteParticle;

    public AudioSource ding;

	void Start ()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        goalManager = GameObject.FindWithTag("GM").GetComponent<GoalManager>();
        pizzeriaScript = GameObject.FindWithTag("Pizzeria").GetComponent<Pizzeria>();
        ding = GetComponent<AudioSource>();
        particleSpawn = transform.Find(GameObject.FindWithTag("ParticleSpawn").name);
        if(gameObject.name != "Church")
        {
            naam = GameObject.FindGameObjectWithTag("GM").GetComponent<RandomNameGen>().Name();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gm.timeToCountDown > 0)
            {
                if (isTarget)
                {
                    StartCoroutine(DeliveredPizzaNotice());
                    //Debug.Log("Pizza Delivered");
                    ding.Play();
                    uim.ResetHouseText();
                    gm.timerOn = false;
                    gm.timeToCountDown = 180F;
                    GivePoints();
                    gm.onMission = false;
                    PizzaQuality.quality = 0;
                    pizzeriaScript.canSpawnParticle = true;
                    DeleteMarker();
                    Instantiate(goalCompleteParticle, particleSpawn);
                    isTarget = false;
                    pizzabox.Inactive();

                    if (goalManager.goal2CurrentAmount < goalManager.goal2AmountToReach)
                    {
                        goalManager.goal2CurrentAmount = goalManager.AddToCurrentAmount(goalManager.goal2CurrentAmount, goalManager.goal2AmountToReach);
                    }
                }
            }
        }
    }

    public void CreateMarker()
    {
        Vector3 vec = new Vector3(transform.position.x, 20, transform.position.z);
        vec.y += 3;
        cloneOne = Instantiate(markOne, vec, Quaternion.identity);
        particle = Instantiate(particlePrefab, particleSpawn);
    }

    public void DeleteMarker()
    {
        Destroy(cloneOne);
        Destroy(particle);
        cloneOne = null;
        particle = null;
    }

    public void GivePoints()
    {
        //if (PizzaQuality.quality > 100)
        //{
        //    GameManager.score += pointsGive + bonusPoints;
        //}
        //else if (PizzaQuality.quality == 100)
        //{
        //    GameManager.score += pointsGive;
        //}
        //else if (PizzaQuality.quality <= 80)
        //{
        //    GameManager.score += pointsGive - 20;
        //}
        //else if (PizzaQuality.quality <= 60)
        //{
        //    GameManager.score += pointsGive - 40;
        //}
        //else if (PizzaQuality.quality <= 40)
        //{
        //    GameManager.score += pointsGive - 60;
        //}
        //else if (PizzaQuality.quality <= 20)
        //{
        //    GameManager.score += pointsGive - 80;
        //}

        if (PizzaQuality.quality > 100)
        {
            GameManager.score += (int)PizzaQuality.quality + bonusPoints;

            if (PizzaQuality.quality >= 150)
            {
                goalManager.goal4CurrentAmount = goalManager.AddToCurrentAmount(goalManager.goal4CurrentAmount, goalManager.goal4AmountToReach);
                goalManager.goal4OnGoing = false;
            }
        }
        else
        {
            GameManager.score += (int)PizzaQuality.quality;
        }
    }

    public IEnumerator DeliveredPizzaNotice()
    {
        uim.orderText.text = "You've successfully delivered the pizza!" + "\n\n" + "Now get back to the pizzeria and go get the next one!";
        uim.orderAnimator.SetBool("Order", true);

        yield return new WaitForSeconds(4);

        uim.orderAnimator.SetBool("Order", false);
    }
}
