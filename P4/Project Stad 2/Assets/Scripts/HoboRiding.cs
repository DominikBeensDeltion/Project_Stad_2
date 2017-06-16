using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboRiding : MonoBehaviour
{

    private GameManager gm;

    public Vector3 currentPosition;
    public Vector3 lastPosition;

    public float startSpeed = 5f;
    public float currentSpeed = 5f;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * currentSpeed));
        currentPosition = transform.position;
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RidingHobo")
        {
            currentSpeed = 0;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "RidingHobo")
        {
            currentSpeed = startSpeed;
        }
    }

    public void TurnLeft()
    {
        lastPosition = currentPosition;

        if (lastPosition.x != currentPosition.x)
        {

        }
        else if (lastPosition.z != currentPosition.z)
        {

        }
    }
}
