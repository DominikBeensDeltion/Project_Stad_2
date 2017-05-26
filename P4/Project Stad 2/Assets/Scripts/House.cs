using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    public UIManager uim;
    public GameManager gm;

    public bool isTarget;
    public GameObject markOne;
    public GameObject cloneOne;
    public string naam;

    public int pointsGive = 100;
	void Start ()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gm.timeToCountDown > 0)
            {
                if (isTarget)
                {
                    Debug.Log("Pizza Delivered");
                    uim.ResetHouseText();
                    gm.timerOn = false;
                    gm.timeToCountDown = 180F;
                    GivePoints();
                    gm.GetComponent<PizzaQuality>().NoPizzaText();

                    DeleteMarker();
                    isTarget = false;
                }
            }
        }
    }

    public void CreateMarker()
    {
        Vector3 vec = new Vector3(transform.position.x, 20, transform.position.z);
        vec.y += 3;
        cloneOne = Instantiate(markOne, vec, Quaternion.identity);
    }

    public void DeleteMarker()
    {
        Destroy(cloneOne);
        cloneOne = null;
    }

    public void GivePoints()
    {
        if (PizzaQuality.quality >= 100)
        {
            GameManager.score += pointsGive;
        }
        else if (PizzaQuality.quality <= 80)
        {
            GameManager.score += pointsGive - 20;
        }
        else if (PizzaQuality.quality <= 60)
        {
            GameManager.score += pointsGive - 40;
        }
        else if (PizzaQuality.quality <= 40)
        {
            GameManager.score += pointsGive - 60;
        }
        else if (PizzaQuality.quality <= 20)
        {
            GameManager.score += pointsGive - 80;
        }
        else if (PizzaQuality.quality <= 0)
        {
            GameManager.score += pointsGive - 100;
        }
    }
}
