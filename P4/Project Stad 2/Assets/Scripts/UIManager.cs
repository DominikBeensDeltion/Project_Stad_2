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
    public Animator introAnimator;

    [Header("Order Panel")]
    public Text orderText;
    public Animator orderAnimator;

    [Header("Timer")]
    public Text timerText;

    [Header("Pause Menu")]
    public bool gamePaused;
    public bool canPause;
    public GameObject pausePanel;
    public Animator pauseAnimator;

    public Text houseText;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();

        introImages = introPanel.GetComponentsInChildren<Image>();
        introText = introPanel.GetComponentsInChildren<Text>();
        IntroMouseExit();
    }

    private void Update()
    {
        timerText.text = gm.timeToCountDown.ToString("0");
    }

    public void IntroMouseEnter()
    {
        for (int i = 0; i < introImages.Length; i++)
        {
            introImages[i].CrossFadeAlpha(1f, 0.3f, false);
            introText[i].CrossFadeAlpha(1f, 0.3f, false);
        }
    }

    public void IntroMouseExit()
    {
        for (int i = 0; i < introImages.Length; i++)
        {
            introImages[i].CrossFadeAlpha(0.25f, 0.3f, false);
            introText[i].CrossFadeAlpha(0.25f, 0.3f, false);
        }
    }

    public void IntroStartButton()
    {
        IntroStart();
    }

    public void IntroStart()
    {
        introCamScript.followPath = false;
        introAnimator.SetTrigger("SetInactive");

        //zet de player visible en zijn controls aan, kon hem niet aan en uit doen met setactive omdat verschillende scripts in start de player zoeken
        gm.player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }

    public IEnumerator PauseGame()
    {
        if (gamePaused == false)
        {
            gamePaused = true;
            canPause = false;

            pauseAnimator.SetTrigger("SetActive");

            yield return new WaitForSeconds(0.6f);

            Time.timeScale = 0;
            canPause = true;
        }
        else if (gamePaused == true)
        {
            Time.timeScale = 1;
            canPause = false;

            pauseAnimator.SetBool("SetInactive", true);

            yield return new WaitForSeconds(0.6f);

            pauseAnimator.SetBool("SetInactive", false);

            gamePaused = false;
            canPause = true;
        }
    }

    public IEnumerator ResumeGame()
    {
        if (gamePaused == true)
        {
            Time.timeScale = 1;
            canPause = false;

            pauseAnimator.SetBool("SetInactive", true);

            yield return new WaitForSeconds(0.6f);

            pauseAnimator.SetBool("SetInactive", false);

            gamePaused = false;
            canPause = true;
        }
    }

    public void TempHouseText(GameObject g)
    {
        if (g.GetComponent<House>().isTarget)
        {
            houseText.text = "Goal: " + g.GetComponent<House>().naam;
        }      
    }

    public void ResetHouseText()
    {
        houseText.text = "Goal: Pizzeria";
    } 
}
