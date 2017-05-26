using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public UIManager uim;
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

    public bool onMission;

    public List<GameObject> pickups = new List<GameObject>();
    public int allowedPickups;
    public static int currentPickups;
    public float xRad;
    public float zRad;

    public float speedToGive;
    public float moveSpeedDuration;

    private void Start()
    {
        gameState = GameState.Intro;
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
        mainCam = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        GeneratePickups();
        if (timeToCountDown <= 0)
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

        player.GetComponent<CharacterController>().enabled = true;

        uim.orderText.text = "Picked up pizza!" + "\n\n" + "Now get delivering!";
        uim.orderAnimator.SetBool("Order", true);
        pizzeria.ChooseHouse();
        uim.canPause = true;
        gameState = GameState.Playing;

        yield return new WaitForSeconds(3);

        uim.orderAnimator.SetBool("Order", false);
    }

    public void ResumeButton()
    {
        StartCoroutine(uim.ResumeGame());
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

    public void GeneratePickups()
    {
        if (currentPickups < allowedPickups)
        {
            int i = Random.Range(0, pickups.Count);
            float rndX = Random.Range(0, xRad);
            float rndZ = Random.Range(0, zRad);
            Vector3 spawnPos = new Vector3(rndX, 1, rndZ);
            RaycastHit hit;
            if (Physics.Raycast(spawnPos, Vector3.down, out hit))
            {
                if(hit.transform.tag == "Road")
                {
                    Instantiate(pickups[i], hit.transform.position, Quaternion.identity);
                    currentPickups += 1;
                }
                else
                {
                    GeneratePickups();
                }
            }
        }
    }

   public IEnumerator RemoveSpeed()
    {
        yield return new WaitForSeconds(moveSpeedDuration);
        player.GetComponent<CharacterController>().moveSpeed -= speedToGive;
    }
}
