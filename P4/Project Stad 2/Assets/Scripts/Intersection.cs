using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    public List<GameObject> hobosWaiting = new List<GameObject>();

    public bool aHoboCanPass = true;
    public int timeToLetNextHoboPass = 2;

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
        hobosWaiting.Remove(hobosWaiting[hoboWhoMayContinue]);

        yield return new WaitForSeconds(timeToLetNextHoboPass);

        aHoboCanPass = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RidingHobo")
        {
            hobosWaiting.Add(other.gameObject);
            other.GetComponent<HoboRiding>().currentSpeed = 0f;
        }
    }
}
