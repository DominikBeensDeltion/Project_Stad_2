using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameManager gm;
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
    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        gm.speedToGive = speedToGive;
        gm.moveSpeedDuration = moveSpeedDuration;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerCar")
        {
            pickedUp = true;
            AudioSource.PlayClipAtPoint(ding, transform.position);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (pickedUp)
        {
                if (giveTime)
                {
                    gm.timeToCountDown += timeToGive;
                    pickedUp = false;
                }
                else if (giveQual)
                {
                    PizzaQuality.quality += QualToGive;
                    pickedUp = false;
                }
            //Not sure about this
            //Error = Coroutine could not be started because the game object PickupSpeed(Clone) is inactive!
            //ook al start ik de coroutine in de gamemanager

            //else if (giveSpeed)
            //{
            //    cr.moveSpeed += speedToGive;
            //    StartCoroutine(gm.RemoveSpeed());
            //    pickedUp = false;
            //}

        }
        GameManager.currentPickups -= 1;
        
    }
}