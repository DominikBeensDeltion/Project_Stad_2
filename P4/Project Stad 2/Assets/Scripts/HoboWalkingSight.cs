using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboWalkingSight : MonoBehaviour
{

    private HoboWalking hoboWalking;
    private GameObject player;

    public float sightRange = 10;
    public float sightAngle = 30;

    private void Start()
    {
        hoboWalking = GetComponentInParent<HoboWalking>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toPlayer = player.transform.position - hoboWalking.transform.position;

        if (Vector3.Distance(hoboWalking.transform.position, player.transform.position) <= sightRange)
        {
            print("player is in range");

            if (Vector3.Dot(forward, toPlayer) > 0)
            {
                print("player is infront of hobo");

                Vector3 targetDir = player.transform.position - hoboWalking.transform.position;
                float angle = Vector3.Angle(targetDir, transform.forward);

                //Debug.DrawRay(hoboWalking.transform.position, hoboWalking.transform.position + new Vector3((300), 0), Color.red);
                //Debug.DrawRay(hoboWalking.transform.position, hoboWalking.transform.position + new Vector3((-300), 0), Color.blue);


                if (angle < sightAngle)
                {
                    print("player is in sight");

                    //Debug.DrawRay(hoboWalking.transform.position, player.transform.position - hoboWalking.transform.position, Color.yellow);

                    hoboWalking.ChasePlayer();
                }
            }
        }
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(hoboWalking.transform.position, other.transform.position - hoboWalking.transform.position, out hit, Vector3.Distance(hoboWalking.transform.position, other.transform.position)))     //dit doet hij ergens helemaal anders naartoe.. na 30 min geef ik het op
    //        {
    //            Debug.DrawRay(hoboWalking.transform.position, other.transform.position - hoboWalking.transform.position, new Color (0,0,0));
    //            if (hit.transform.gameObject.tag == "Player")
    //            {
    //                print("chase");
    //                hoboWalking.ChasePlayer();
    //            }
    //        }
    //    }
    //}
}
