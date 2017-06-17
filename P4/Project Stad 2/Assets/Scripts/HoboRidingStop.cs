using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboRidingStop : MonoBehaviour
{

    private HoboRiding hoboriding;

    private void Start()
    {
        hoboriding = GetComponentInParent<HoboRiding>();
    }

    public void OnTriggerEnter(Collider other)
     {
         if (other.tag == "RidingHobo")
         {
            hoboriding.currentSpeed = 0;
         }
     }
 
    public void OnTriggerExit(Collider other)
    {
         if (other.tag == "RidingHobo")
         {
            hoboriding.currentSpeed = hoboriding.startSpeed;
         }
     }
}
