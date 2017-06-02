using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public UIManager uim;
    public Pizzeria pizzeria;

    public enum GameState
    {
        Intro,
        Playing,
        InPizzeria,
        Paused,
        Dead
    }
    public GameState gameState;

    public GameObject player;
    public GameObject mainCam;

    public bool timerOn;
    public float timeToCountDown = 180f;

    public static int score;

    public bool onMission;

    public bool deathHoboRiding;
    public bool deathOutOfTime;
    public bool deathPizzaEaten;

    public PizzaCar carScript;
    public string[] carCheat;
    public int carCheatIndex;
    public bool canInputCheat = true;

    private void Start()
    {
        gameState = GameState.Intro;
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        mainCam = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
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

        if (gameState == GameState.Paused && canInputCheat)
        {
            if (Input.GetKeyDown(carCheat[carCheatIndex]))
            {
                if (carCheatIndex != carCheat.Length - 1)
                {
                    carCheatIndex++;
                }
                else if (carCheatIndex == carCheat.Length - 1)
                {
                    carScript.repaired = true;
                    canInputCheat = false;
                }
            }
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
}
