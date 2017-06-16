using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboRiding : MonoBehaviour
{

    private GameManager gm;
    public bool waitingAtIntersection;

    public float startSpeed = 5f;
    public float currentSpeed = 5f;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * currentSpeed));

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.forward, out hit, 2))
        {
            print("test");
            if (hit.transform.tag == "RidingHobo")
            {
                currentSpeed = 0;
            }
            else if (hit.transform.tag != "RidingHobo")
            {
                if (waitingAtIntersection)
                {
                    return;
                }
                else if (!waitingAtIntersection)
                {
                    currentSpeed = startSpeed;
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (gm.gameState == GameManager.GameState.Playing)
        {
            if (collision.gameObject.tag == "Player")
            {
                gm.deathHoboRiding = true;
                gm.GameOver();
            }
        }
    }
}
