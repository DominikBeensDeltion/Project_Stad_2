using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaQuality : MonoBehaviour {
    private GameManager gm;
    private UIManager ui;
    public static float quality;

	void Start ()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        ui = GameObject.FindGameObjectWithTag("UIM").GetComponent<UIManager>();
    }
	
	void Update ()
    {
        if(quality > 0)
        {
            DisplayQual();
        }
        else
        {
            NoPizzaText();
        }

        if (gm.gameState == GameManager.GameState.Playing)
        {
            if (quality <= 0)
            {
                if (gm.onMission)
                {
                    gm.deathPizzaEaten = true;
                    gm.GameOver();
                }
            }         
        }
    }

    void DisplayQual()
    {
        ui.qualityText.text = "" + quality + "%";
    }

    public void NoPizzaText()
    {
        ui.qualityText.text = "N/A";
    }
}
