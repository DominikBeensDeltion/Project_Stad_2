using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzabox : MonoBehaviour
{

    public GameObject hands;
    public GameObject pizzabox;
    public float speed = 15;
    public ParticleSystem getPizzaParticle;

    private void Update()
    {
        pizzabox.transform.position = Vector3.Lerp(pizzabox.transform.transform.position, hands.transform.position, (speed * Time.deltaTime));
    }

    public IEnumerator Active()
    {
        yield return new WaitForSeconds(0.5f);
        pizzabox.SetActive(true);
        getPizzaParticle.Play();
    }

    public void Inactive()
    {
        pizzabox.SetActive(false);
    }
}
