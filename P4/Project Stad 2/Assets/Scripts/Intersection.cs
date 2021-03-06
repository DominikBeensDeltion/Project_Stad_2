﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    public List<GameObject> hobosWaiting = new List<GameObject>();

    public bool aHoboCanPass = true;
    public float timeToLetNextHoboPass = 2;

    private void Update()
    {
        if (hobosWaiting.Count >= 1)
        {
            if (aHoboCanPass)
            {
                StartCoroutine(LetHoboThrough());
            }
        }
    }

    public IEnumerator LetHoboThrough()
    {
        aHoboCanPass = false;

        int hoboWhoMayContinue = Random.Range(0, hobosWaiting.Count);

        hobosWaiting[hoboWhoMayContinue].GetComponent<HoboRiding>().currentSpeed = hobosWaiting[hoboWhoMayContinue].GetComponent<HoboRiding>().startSpeed;
        //hobosWaiting[hoboWhoMayContinue].GetComponent<HoboRiding>().waitingAtIntersection = false;
        hobosWaiting[hoboWhoMayContinue].GetComponent<HoboRiding>().ignoringOthers = true;
        hobosWaiting.Remove(hobosWaiting[hoboWhoMayContinue]);

        yield return new WaitForSeconds(timeToLetNextHoboPass);

        aHoboCanPass = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RidingHobo")
        {
            hobosWaiting.Add(other.gameObject);
            other.GetComponent<HoboRiding>().currentSpeed = 0f;
            //other.GetComponent<HoboRiding>().waitingAtIntersection = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "RidingHobo")
        {
            other.GetComponent<HoboRiding>().ignoringOthers = false;
        }
    }
}
