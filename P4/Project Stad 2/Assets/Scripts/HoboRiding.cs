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

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        myTransform.Translate(Vector3.forward * (Time.deltaTime * currentSpeed));
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
