using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private GameManager gm;

    [Header("Intro")]
    public IntroCameraMovement introCamScript;
    public GameObject introPanel;
    public Image[] introImages;
    public Text[] introText;
    public Image[] minimapImages;
    public Animator introAnimator;

    [Header("Order Panel")]
    public Text orderText;
    public Animator orderAnimator;

    [Header("Notice Panel")]
    public Animator noticeAnimator;
    public Text noticeText;
    public bool noticePanelIsActive;

    [Header("Mini/World Map")]
    public Animator worldmapAnimator;
    public GameObject worldmapObject;
    public bool worldmapActive;
    public bool canToggleMap;

    public GameObject minimapPanel;
    public Animator minimapAnimator;
    public bool minimapActive = true;
    public RawImage minimapImage;

    [Header("Timer")]
    public Text timerText;

    [Header("Pause Menu")]
    public bool gamePaused;
    public bool canPause;
    public GameObject pausePanel;
    public Animator pauseAnimator;

    [Header("Game Over")]
    public Text gameOverText;
    public Animator gameOverAnimator;
    public Text endScoreText;
    public Text highScoreText;

    private SaveManager saveManager;

    [Header("House")]
    public Text houseText;

    [Header("Pizza Quality")]
    public Text qualityText;

    [Header("Score")]
    public Text scoreText;

    [Header("Phone")]
    public Animator phoneAnimator;
    public bool phoneActive;

    [Header("Goal List")]
    public GameObject goalPanel;
    public bool goalPanelOpen;

    public Text goal1;
    public Text goal2;
    public Text goal3;
    public Text goal4;
    public Text goal5;
    public Text goal6;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        saveManager = gm.GetComponent<SaveManager>();

        introImages = introPanel.GetComponentsInChildren<Image>();
        introText = introPanel.GetComponentsInChildren<Text>();
        minimapImages = minimapPanel.GetComponentsInChildren<Image>();
        IntroMouseExit();
    }

    private void Update()
    {
        scoreText.text = "" + GameManager.score;
        timerText.text = gm.timeToCountDown.ToString("0");

        if (gm.gameState == GameManager.GameState.Playing)
        {
            if (Input.GetButtonDown("m") || Input.GetButtonDown("Tab"))
            {
                if (canToggleMap)
                {
                    if (!worldmapActive)
                    {
                        StartCoroutine(WorldMapRefresh());
                        StartCoroutine(OpenWorldmap());
                    }
                    else if (worldmapActive)
                    {
                        StartCoroutine(CloseWorldmap());
                    }
                }
            }

            if (Input.GetButtonDown("Shift"))
            {
                if (!phoneActive)
                {
                    phoneAnimator.SetBool("PhoneActive", true);
                    phoneAnimator.SetBool("PhoneInActive", false);
                    phoneActive = true;
                }
                else if (phoneActive)
                {
                    phoneAnimator.SetBool("PhoneActive", false);
                    phoneAnimator.SetBool("PhoneInActive", true);
                    phoneActive = false;
                }
            }
        }
    }

    public IEnumerator OpenWorldmap()
    {
        canToggleMap = false;
        if (minimapActive)
        {
            minimapAnimator.SetTrigger("SetInactive");
            minimapActive = false;
        }
        yield return new WaitForSeconds(0.3f);
        worldmapAnimator.SetTrigger("SetActive");
        yield return new WaitForSeconds(0.3f);
        worldmapActive = true;
        canToggleMap = true;
    }

    public IEnumerator CloseWorldmap()
    {
        canToggleMap = false;
        worldmapAnimator.SetTrigger("SetInactive");
        yield return new WaitForSeconds(0.3f);
        if (!minimapActive)
        {
            minimapAnimator.SetTrigger("SetActive");
            minimapActive = true;
        }
        yield return new WaitForSeconds(0.3f);
        worldmapActive = false;
        canToggleMap = true;
    }

    public IEnumerator WorldMapRefresh()
    {
        worldmapObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        worldmapObject.SetActive(false);
    }

    public void IntroMouseEnter()
    {
        if (gm.gameState == GameManager.GameState.Intro)
        {
            for (int i = 0; i < introImages.Length; i++)
            {
                introImages[i].CrossFadeAlpha(1f, 0.3f, false);
            }
            for (int i = 0; i < introText.Length; i++)
            {
                introText[i].CrossFadeAlpha(1f, 0.3f, false);
            }
            for (int i = 0; i < minimapImages.Length; i++)
            {
                minimapImages[i].CrossFadeAlpha(1f, 0.3f, false);
            }

            minimapImage.CrossFadeAlpha(1f, 0.3f, false);
        }
    }

    public void IntroMouseExit()
    {
        if (gm.gameState == GameManager.GameState.Intro)
        {
            for (int i = 0; i < introImages.Length; i++)
            {
                introImages[i].CrossFadeAlpha(0.15f, 0.3f, false);
            }
            for (int i = 0; i < introText.Length; i++)
            {
                introText[i].CrossFadeAlpha(0.15f, 0.3f, false);
            }
            for (int i = 0; i < minimapImages.Length; i++)
            {
                minimapImages[i].CrossFadeAlpha(0.15f, 0.3f, false);
            }

            minimapImage.CrossFadeAlpha(0.15f, 0.3f, false);
        }
    }

    public void IntroStartButton()
    {
        StartCoroutine(IntroStart());
    }

    public IEnumerator IntroStart()
    {
        Time.timeScale = 1;
        GameManager.score = 0;
        introCamScript.followPath = false;
        introAnimator.SetTrigger("SetInactive");
        //zet de player visible en zijn controls aan, kon hem niet aan en uit doen met setactive omdat verschillende scripts in start de player zoeken
        gm.player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

        yield return new WaitForSeconds(0.5f);
        introPanel.SetActive(false);

        for (int i = 0; i < minimapImages.Length; i++)
        {
            minimapImages[i].CrossFadeAlpha(1f, 0.3f, false);
        }

        minimapImage.CrossFadeAlpha(1f, 0.3f, false);
    }

    public IEnumerator PauseGame()
    {
        if (gm.gameState != GameManager.GameState.Paused)
        {
            gm.gameState = GameManager.GameState.Paused;
            gamePaused = true;
            canPause = false;

            pauseAnimator.SetTrigger("SetActive");

            yield return new WaitForSeconds(0.3f);

            Time.timeScale = 0;
            canPause = true;
        }
        else if (gm.gameState == GameManager.GameState.Paused)
        {
            Time.timeScale = 1;
            canPause = false;

            pauseAnimator.SetBool("SetInactive", true);

            yield return new WaitForSeconds(0.3f);

            pauseAnimator.SetBool("SetInactive", false);

            gamePaused = false;
            gm.gameState = GameManager.GameState.Playing;
            canPause = true;
        }
    }

    public IEnumerator ResumeGame()
    {
        if (gm.gameState == GameManager.GameState.Paused)
        {
            Time.timeScale = 1;
            canPause = false;

            pauseAnimator.SetBool("SetInactive", true);

            yield return new WaitForSeconds(0.6f);

            pauseAnimator.SetBool("SetInactive", false);

            gamePaused = false;
            gm.gameState = GameManager.GameState.Playing;
            canPause = true;
        }
    }

    public IEnumerator GameOver()
    {
        if (phoneActive)
        {
            phoneAnimator.SetBool("PhoneActive", false);
            phoneAnimator.SetBool("PhoneInActive", true);
            phoneActive = false;
        }

        if (gm.deathHoboRiding)
        {
            gameOverText.text = "You were ran over!";
        }
        else if (gm.deathPizzaEaten)
        {
            gameOverText.text = "Your pizza was eaten!";
        }
        else if (gm.deathOutOfTime)
        {
            gameOverText.text = "You ran out of time!";
        }

        endScoreText.text = "" + GameManager.score;

        if (GameManager.score > saveManager.saveData.highScore)
        {
            saveManager.saveData.highScore = GameManager.score;
            highScoreText.text = "" + saveManager.saveData.highScore + "\nNew High Score!";
            saveManager.Save(saveManager.saveData);
        }
        else
        {
            highScoreText.text = "" + saveManager.saveData.highScore;
        }

        gameOverAnimator.SetTrigger("SetActive");

        yield return new WaitForSeconds(0.6f);

        Time.timeScale = 0;
    }

    public void HouseText(GameObject g)
    {
        if (g.GetComponent<House>().isTarget)
        {
            houseText.text = g.GetComponent<House>().naam;
        }      
    }

    public void ResetHouseText()
    {
        houseText.text = "Pizzeria";
    } 
}
