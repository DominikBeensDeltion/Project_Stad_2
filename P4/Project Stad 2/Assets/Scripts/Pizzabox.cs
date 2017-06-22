using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzabox : MonoBehaviour
{

    public GameObject hands;
    public GameObject pizzabox;
    public float speed = 15;

    public Transform particleSpawn;
    public GameObject getPizzaParticle;
    public GameObject getHitPizzaParticle;

    private void Update()
    {
        pizzabox.transform.position = Vector3.Lerp(pizzabox.transform.transform.position, hands.transform.position, (speed * Time.deltaTime));
    }

    public IEnumerator Active()
    {
        yield return new WaitForSeconds(0.4f);
        pizzabox.SetActive(true);
        Instantiate(getPizzaParticle, particleSpawn.position, particleSpawn.rotation).transform.SetParent(particleSpawn);
    }

    public void Inactive()
    {
        pizzabox.SetActive(false);
    }

    public void GetHit()
    {
        Instantiate(getHitPizzaParticle, particleSpawn.position, particleSpawn.rotation).transform.SetParent(particleSpawn);
    }
}
