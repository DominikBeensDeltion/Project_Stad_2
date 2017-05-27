using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzeriaEntrance : MonoBehaviour
{

    private GameObject player;
    public Pizzeria pizzeriaScript;

    public GameObject outsidePizzeriaSpawn;
    public GameObject insidePizzeriaSpawn;
    public GameObject playerCam;
    public GameObject pizzeriaCam;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("e"))
            {
                if (pizzeriaScript.playerInsidePizzeria)
                {
                    player.transform.position = outsidePizzeriaSpawn.transform.position;
                    pizzeriaCam.SetActive(false);
                    playerCam.SetActive(true);
                    pizzeriaScript.playerInsidePizzeria = false;
                }
                else if (!pizzeriaScript.playerInsidePizzeria)
                {
                    player.transform.position = insidePizzeriaSpawn.transform.position;
                    pizzeriaCam.SetActive(true);
                    playerCam.SetActive(false);
                    pizzeriaScript.playerInsidePizzeria = true;
                }
            }
        }
    }
}
