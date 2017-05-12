using System.Collections;
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

    public void GameOver()
    {
        Debug.Log("u suck");
    }
}
