using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameManager gm;

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
    }

    private void Update()
    {
        timerText.text = gm.timeToCountDown.ToString("0");
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
