using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameManager gm;

    [Header("Timer")]
    public Text timerText;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }

    private void Update()
    {
        timerText.text = gm.timeToCountDown.ToString("0");
    }
}
