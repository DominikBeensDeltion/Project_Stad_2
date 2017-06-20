using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboRiding : MonoBehaviour
{

    private GameManager gm;
    public bool waitingAtIntersection;
    public bool ignoringOthers;
    public bool stopped;

    private Transform myTransform;
    public float startSpeed = 5f;
    public float currentSpeed = 5f;

    public bool goLeft;
    public bool canTurn;
    public Vector3 position;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        myTransform.Translate(Vector3.forward * (Time.deltaTime * currentSpeed));

        if (goLeft)
        {
            if (myTransform.position.x >= position.x + 2 || myTransform.position.x <= position.x - 2)
            {
                if (canTurn)
                {
                    myTransform.Rotate(0, -90, 0);
                    canTurn = false;
                    goLeft = false;
                }
            }
            else if (myTransform.position.z >= position.z + 2 || myTransform.position.z <= position.z - 2)
            {
                if (canTurn)
                {
                    myTransform.Rotate(0, -90, 0);
                    canTurn = false;
                    goLeft = false;
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

    public void GoLeft()
    {
        position = myTransform.position;
        goLeft = true;
        canTurn = true;
    }
}
