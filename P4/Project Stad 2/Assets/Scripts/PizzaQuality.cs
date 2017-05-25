using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaQuality : MonoBehaviour {
    private GameManager gm;
    private UIManager ui;
    public static float quality;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        ui = GameObject.FindGameObjectWithTag("UIM").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {
        DisplayQual();
        if (quality <= 0)
        {
            gm.GameOver();
        }
    }

    void DisplayQual()
    {
        ui.qualityText.text = "" + quality;
    }
}
