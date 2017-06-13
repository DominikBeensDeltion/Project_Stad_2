using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzabox : MonoBehaviour
{

    public GameObject pizzabox;
    public ParticleSystem getPizzaParticle;

    public void Active()
    {
        pizzabox.SetActive(true);
        getPizzaParticle.Play();
    }

    public void Inactive()
    {
        pizzabox.SetActive(false);
    }
}
