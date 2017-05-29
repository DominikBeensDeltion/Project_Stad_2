using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;
    private PickupManager pm;
    public AudioClip ding;
    public CharacterController cr;

    public bool giveTime;
    public bool giveQual;
    public bool giveSpeed;
    public bool pickedUp;

    public float timeToGive;
    public float speedToGive;
    public float QualToGive;

    public int moveSpeedDuration;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        pm = GameObject.FindGameObjectWithTag("GM").GetComponent<PickupManager>();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerCar")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            //pickedUp = true;
            AudioSource.PlayClipAtPoint(ding, transform.position);

            //if (pickedUp)
            //{
                if (gm.onMission)
                {
                    if (giveTime)
                    {
                        StartCoroutine(TimePickup());
                    }
                    if (giveQual)
                    {
                        StartCoroutine(QualityPickup());
                    }
                    //Not sure about this
                    //Error = Coroutine could not be started because the game object PickupSpeed(Clone) is inactive!
                    //ook al start ik de coroutine in de gamemanager
                    if (giveSpeed)
                    {
                        StartCoroutine(SpeedPickup());
                    }
             //}
            }
        }
    }

    public IEnumerator TimePickup()
    {
        gm.timeToCountDown += timeToGive;
        //pickedUp = false;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    public IEnumerator QualityPickup()
    {
        PizzaQuality.quality += QualToGive;
        //pickedUp = false;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    public IEnumerator SpeedPickup()
    {
        cr.moveSpeed += speedToGive;
        //pickedUp = false;

        yield return new WaitForSeconds(moveSpeedDuration);

        cr.moveSpeed -= speedToGive;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        PickupManager.currentPickups -= 1;       
    }
}