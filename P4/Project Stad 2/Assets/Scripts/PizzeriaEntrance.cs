using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzeriaEntrance : MonoBehaviour
{

    private UIManager uim;
    private GameObject player;
    public Pizzeria pizzeriaScript;

    public static bool canEnterExit = true;
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
            if (!uim.noticeAnimator.GetCurrentAnimatorStateInfo(0).IsName("NoticeActive"))
            {
                uim.noticeAnimator.SetTrigger("SetActive");
                uim.noticePanelIsActive = true;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("e"))
            {
                if (Pizzeria.playerInsidePizzeria && canEnterExit)
                {
                    player.transform.position = outsidePizzeriaSpawn.transform.position;
                    canEnterExit = false;
                    StartCoroutine(Cooldown());
                    pizzeriaCam.SetActive(false);
                    playerCam.SetActive(true);
                    Pizzeria.playerInsidePizzeria = false;
                }
                else if (!Pizzeria.playerInsidePizzeria && canEnterExit)
                {
                    player.transform.position = insidePizzeriaSpawn.transform.position;
                    canEnterExit = false;
                    StartCoroutine(Cooldown());
                    pizzeriaCam.SetActive(true);
                    playerCam.SetActive(false);
                    Pizzeria.playerInsidePizzeria = true;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (uim.noticePanelIsActive && !uim.noticeAnimator.GetCurrentAnimatorStateInfo(0).IsName("NoticeInActive"))
            {
                uim.noticeAnimator.SetTrigger("SetInActive");
                uim.noticePanelIsActive = false;
            }
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        canEnterExit = true;
    }
}
