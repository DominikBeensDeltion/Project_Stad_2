using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worldmap : MonoBehaviour {
    public bool open;
    public GameObject worldCam;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        OpenCloseMap();

    }

    void OpenCloseMap()
    {

        if (Input.GetButtonDown("m"))
        {
            if (!open)
            {
                worldCam.SetActive(true);
                open = true;
            }
            else if (open)
            {
                if (Input.GetButtonDown("m"))
                {
                    worldCam.SetActive(false);
                    open = false;
                }
            }
            
        }
    }
}
