﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public UIManager uim;
    public bool timerOn;
    public float timeToCountDown = 180f;

    private void Start()
    {
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    private void Update()
    {
        if(timeToCountDown <= 0)
        {
            GameOver();
        }
        if (timerOn)
        {
            if(timeToCountDown > 0)
            {
                timeToCountDown -= Time.deltaTime;
            } 
        }

        if (Input.GetKeyDown(KeyCode.Escape) && uim.canPause)
        {
            StartCoroutine(uim.PauseGame());
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);

        uim.orderText.text = "Picked up pizza!" + "\n\n" + "Now get delivering!";
        uim.orderAnimator.SetBool("Order", true);

        yield return new WaitForSeconds(3);

        uim.orderAnimator.SetBool("Order", false);

        yield return new WaitForSeconds(1);

        //timerOn = true;
    }

    public void GameOver()
    {
        Debug.Log("u suck");
    }
}
