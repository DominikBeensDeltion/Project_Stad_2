using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    public bool isTarget;
    public UIManager ui;
    public GameManager gm;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gm.timeToCountDown > 0)
            {
                if (isTarget)
                {
                    Debug.Log("Pizza Delivered");
                    ui.ResetHouseText();
                    gm.timerOn = false;
                    gm.timeToCountDown = 180F;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isTarget)
            {
                isTarget = false;
            }
        }
    }
}
