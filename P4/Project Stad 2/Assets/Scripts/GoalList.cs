using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalList : MonoBehaviour
{

    private UIManager uim;

    private void Start()
    {
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!uim.noticePanelIsActive)
            {
                uim.noticeText.text = "Press E to see your goals";
                uim.noticeAnimator.SetTrigger("SetActive");
                uim.noticePanelIsActive = true;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!uim.goalPanelOpen)
            {
                if (Input.GetButtonDown("e"))
                {
                    uim.goalPanel.SetActive(true);
                    uim.goalPanelOpen = true;

                    if (uim.noticePanelIsActive)
                    {
                        uim.noticeAnimator.SetTrigger("SetInActive");
                        uim.noticePanelIsActive = false;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (uim.goalPanelOpen)
            {
                uim.goalPanel.SetActive(false);
                uim.goalPanelOpen = false;
            }

            if (uim.noticePanelIsActive)
            {
                uim.noticeAnimator.SetTrigger("SetInActive");
                uim.noticePanelIsActive = false;
            }
        }
    }

    public void ClosePanelButton()
    {
        uim.goalPanel.SetActive(false);
        uim.goalPanelOpen = false;
    }
}
