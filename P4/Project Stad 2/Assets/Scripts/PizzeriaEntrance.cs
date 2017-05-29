using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzeriaEntrance : MonoBehaviour
{

    private UIManager uim;
    private GameObject player;
    public Pizzeria pizzeriaScript;

    public GameObject outsidePizzeriaSpawn;
    public GameObject insidePizzeriaSpawn;
    public GameObject playerCam;
    public GameObject pizzeriaCam;

    private void Start()
    {
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        player = GameObject.FindWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uim.noticeText.text = "Press E to enter/exit pizzeria";
            uim.noticeAnimator.SetTrigger("SetActive");
            uim.noticePanelIsActive = true;
        }
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

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (uim.noticePanelIsActive)
            {
                uim.noticeAnimator.SetTrigger("SetInActive");
                uim.noticePanelIsActive = false;
            }
        }
    }
}
