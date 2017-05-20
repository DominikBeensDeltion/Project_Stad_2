using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboWalkingSight : MonoBehaviour
{

    private HoboWalking hoboWalking;

    private void Start()
    {
        hoboWalking = GetComponentInParent<HoboWalking>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Raycast(hoboWalking.transform.position, other.transform.position, out hit, Vector3.Distance(hoboWalking.transform.position, other.transform.position)))     //dit doet hij ergens helemaal anders naartoe.. na 30 min geef ik het op
            {
                //Debug.DrawRay(transform.position, other.transform.position, new Color (0,0,0));
                if (hit.transform.tag == "Player")
                {
                    hoboWalking.ChasePlayer();
                }
            }
        }
    }
}
