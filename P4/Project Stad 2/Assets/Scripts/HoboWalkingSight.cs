using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboWalkingSight : MonoBehaviour
{

    private HoboWalking hoboWalking;
    public LayerMask lm;

    private void Start()
    {
        hoboWalking = GetComponentInParent<HoboWalking>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Raycast(hoboWalking.transform.position, hoboWalking.transform.position - other.transform.position, out hit, Vector3.Distance(hoboWalking.transform.position, other.transform.position), lm))     //dit doet hij ergens helemaal anders naartoe.. na 30 min geef ik het op
            {
                Debug.DrawRay(hoboWalking.transform.position, -hoboWalking.transform.position - -other.transform.position, new Color (0,0,0));
                if (hit.transform.gameObject.tag == "Player")
                {
                    print("chase");
                    hoboWalking.ChasePlayer();
                }
            }
        }
    }
}
