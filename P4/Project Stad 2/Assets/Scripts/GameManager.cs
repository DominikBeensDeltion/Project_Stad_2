﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private UIManager uim;
    public Pizzeria pizzeria;

    public enum GameState
    {
        Intro,
        Playing,
        Paused,
        Dead
    }
    public GameState gameState;

    public GameObject player;
    public GameObject mainCam;

    public bool timerOn;
    public float timeToCountDown = 180f;

    public static int score;
    public static int highScore;

    public bool onMission;

    public bool deathHoboRiding;
    public bool deathOutOfTime;
    public bool deathPizzaEaten;

    public PizzaCar carScript;
    public string[] carCheat;
    public int carCheatIndex;
    public bool canInputCarCheat = true;

    public string[] speedCheat;
    public int speedCheatIndex;
    public bool canInputSpeedCheat = true;

    public string[] repairCarCheat;
    public int repairCarCheatIndex;

    public Shader defaultShader;
    public Shader silhouetteShader;
    public Renderer playerRenderer;
    public Renderer pizzaboxRenderer;

    private void Start()
    {
        gameState = GameState.Intro;
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        mainCam = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");

        defaultShader = player.GetComponentInChildren<Renderer>().material.shader;
    }

    private void Update()
    {
        if (timeToCountDown <= 0)
        {
            deathOutOfTime = true;
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

        if (gameState == GameState.Paused)
        {
            if (canInputCarCheat)
            {
                if (Input.GetKeyDown(carCheat[carCheatIndex]))
                {
                    if (carCheatIndex != carCheat.Length - 1)
                    {
                        carCheatIndex++;
                    }
                    else if (carCheatIndex == carCheat.Length - 1)
                    {
                        carScript.UnlockCar();
                        canInputCarCheat = false;
                    }
                }
            }

            if (canInputSpeedCheat)
            {
                if (Input.GetKeyDown(speedCheat[speedCheatIndex]))
                {
                    if (speedCheatIndex != speedCheat.Length - 1)
                    {
                        speedCheatIndex++;
                    }
                    else if (speedCheatIndex == speedCheat.Length - 1)
                    {
                        player.GetComponent<CharacterController>().moveSpeed = 1500;
                        canInputSpeedCheat = false;
                    }
                }
            }

            if (Input.GetKeyDown(repairCarCheat[repairCarCheatIndex]))
            {
                if (repairCarCheatIndex != repairCarCheat.Length -1)
                {
                    repairCarCheatIndex++;
                }
                else if (repairCarCheatIndex == repairCarCheat.Length -1)
                {
                    carScript.durability = 100f;
                    carScript.currentMoveSpeed = 16f;
                    carScript.brokenParticle.Stop();

                    repairCarCheatIndex = 0;
                }
            }

        }

        if (gameState == GameState.Playing)
        {
            if (uim.worldmapActive || uim.goalPanelOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);

        player.GetComponent<CharacterController>().enabled = true;

        uim.orderText.text = "Picked up pizza!" + "\n\n" + "Now get delivering!";
        uim.orderAnimator.SetBool("Order", true);
        pizzeria.ChooseHouse();
        uim.canPause = true;
        uim.canToggleMap = true;
        gameState = GameState.Playing;

        yield return new WaitForSeconds(3);

        uim.orderAnimator.SetBool("Order", false);
    }

    public void ResumeButton()
    {
        StartCoroutine(uim.ResumeGame());
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameState = GameState.Dead;

        StartCoroutine(uim.GameOver());
    }

    public void ChangeShaderButton()
    {
        if (playerRenderer.material.shader == defaultShader && pizzaboxRenderer.material.shader == defaultShader)
        {
            playerRenderer.material.shader = silhouetteShader;
            pizzaboxRenderer.materials[0].shader = silhouetteShader;
            pizzaboxRenderer.materials[1].shader = silhouetteShader;
        }
        else
        {
            playerRenderer.material.shader = defaultShader;
            pizzaboxRenderer.materials[0].shader = defaultShader;
            pizzaboxRenderer.materials[1].shader = defaultShader;
        }
    }
}
