﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;
    private PickupManager pm;
    private CharacterController cr;
    public AudioClip ding;

    public bool pickedUp;

    public bool giveTime;
    public bool giveQual;
    public bool giveSpeed;

    public float timeToGive;
    public float speedToGive;
    public float QualToGive;

    public int moveSpeedDuration;

    public GameObject particlePrefab;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerCar")
        {
            if (!pickedUp)
            {
                pickedUp = true;
                GetComponent<SpriteRenderer>().enabled = false;
                AudioSource.PlayClipAtPoint(ding, transform.position);
                Instantiate(particlePrefab, transform.position, Quaternion.identity);

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
                    if (giveSpeed)
                    {
                        StartCoroutine(SpeedPickup());
                    }
                }
            }
        }
    }

    public IEnumerator TimePickup()
    {
        gm.timeToCountDown += timeToGive;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    public IEnumerator QualityPickup()
    {
        PizzaQuality.quality += QualToGive;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    public IEnumerator SpeedPickup()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

            cr.moveSpeed += speedToGive;

            yield return new WaitForSeconds(moveSpeedDuration);

            cr.moveSpeed -= speedToGive;

            yield return new WaitForSeconds(0.5f);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        PickupManager.currentPickups -= 1;       
    }
}