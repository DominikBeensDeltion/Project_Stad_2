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
    public bool canPause = true;
    public Image fadeBackground;

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
    }

    public IEnumerator PauseGame()
    {
        if (gamePaused == false)
        {
            gamePaused = true;
            canPause = false;

            fadeBackground.enabled = true;

            fadeBackground.canvasRenderer.SetAlpha(0.1f);
            fadeBackground.CrossFadeAlpha(1f, 1f, false);

            yield return new WaitForSeconds(1.0f);

            Time.timeScale = 0;
            canPause = true;
        }
        else if (gamePaused == true)
        {
            Time.timeScale = 1;
            canPause = false;

            fadeBackground.canvasRenderer.SetAlpha(1.0f);
            fadeBackground.CrossFadeAlpha(0f, 1f, false);

            yield return new WaitForSeconds(1.0f);

            fadeBackground.enabled = false;

            gamePaused = false;
            canPause = true;
        }
    }

    public void TempHouseText(GameObject g)
    {
        if (g.GetComponent<House>().isTarget)
        {
            houseText.text = "Goal: " + g.name;
        }      
    }

    public void ResetHouseText()
    {
        houseText.text = "Goal: Pizzeria";
    } 
}
