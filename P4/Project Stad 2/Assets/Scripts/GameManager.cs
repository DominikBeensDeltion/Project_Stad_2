using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public UIManager uim;

    public bool timerOn;
    public float timeToCountDown = 180f;

    private void Start()
    {
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (timerOn)
        {
            timeToCountDown -= Time.deltaTime;
        }
    }
}
