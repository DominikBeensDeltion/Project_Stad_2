using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public UIManager uim;

    public int goal1CurrentAmount;
    public int goal2CurrentAmount;
    public int goal3CurrentAmount;
    public int goal4CurrentAmount;
    public int goal5CurrentAmount;
    public int goal6CurrentAmount;

    public int goal1AmountToReach = 1000;
    public int goal2AmountToReach = 25;
    public int goal3AmountToReach = 15;
    public int goal4AmountToReach = 1;
    public int goal5AmountToReach = 2;
    public int goal6AmountToReach = 50;

    private void Start()
    {
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    private void Update()
    {
        uim.goal1.text = goal1CurrentAmount + " l " + goal1AmountToReach;
        uim.goal2.text = goal2CurrentAmount + " l " + goal2AmountToReach;
        uim.goal3.text = goal3CurrentAmount + " l " + goal3AmountToReach;
        uim.goal4.text = goal4CurrentAmount + " l " + goal4AmountToReach;
        uim.goal5.text = goal5CurrentAmount + " l " + goal5AmountToReach;
        uim.goal6.text = goal6CurrentAmount + " l " + goal6AmountToReach;
    }
}
