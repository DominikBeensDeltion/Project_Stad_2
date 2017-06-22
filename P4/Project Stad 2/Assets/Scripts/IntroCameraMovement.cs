using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCameraMovement : MonoBehaviour
{

    public GameManager gm;

    public bool introCamera = true;
    public bool followPath = true;

    public Transform[] waypoints;
    public int currentWaypoint = 1;

    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float moveBackSpeed = 10f;
    public float rotateBackSpeed = 20f;

    public Transform mainCamStartPos;
    public Transform mainCamPos;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();

        waypoints = GameObject.FindWithTag("IntroCamWaypoints").GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (gm.gameState == GameManager.GameState.Intro)
        {
            if (introCamera)
            {
                if (followPath)
                {
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[currentWaypoint].transform.position, (moveSpeed * Time.deltaTime));

                    if (gameObject.transform.position == waypoints[currentWaypoint].transform.position)
                    {
                        currentWaypoint++;
                    }

                    if (currentWaypoint == waypoints.Length)
                    {
                        currentWaypoint = 1;
                    }
                }
                else if (!followPath)
                {
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, mainCamPos.position, (moveBackSpeed * Time.deltaTime));

                    if (transform.position == mainCamPos.position && transform.rotation == mainCamPos.rotation)
                    {
                        StartCoroutine(gm.StartGame());
                        introCamera = false;
                    }
                }
            }
        }
    }
}
