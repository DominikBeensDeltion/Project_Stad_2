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
         if (other.tag == "RidingHobo" || other.tag == "PlayerCar")
         {
            if (!hoboriding.ignoringOthers && !hoboriding.stopped)
            {
                hoboriding.currentSpeed = 0;
                hoboriding.stopped = true;
            }
         }
     }
 
    public void OnTriggerExit(Collider other)
    {
         if (other.tag == "RidingHobo" || other.tag == "PlayerCar")
         {
            if (hoboriding.stopped)
            {
                hoboriding.currentSpeed = hoboriding.startSpeed;
                hoboriding.stopped = false;
            }
         }
     }
}
