using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public UIManager uim;

    [Header("Current Goal Amounts")]
    public int goal1CurrentAmount;
    public int goal2CurrentAmount;
    public int goal3CurrentAmount;
    public int goal4CurrentAmount;
    public int goal5CurrentAmount;
    public int goal6CurrentAmount;

    [Header("Goal To Reach Amounts")]
    public int goal1AmountToReach = 1000;
    public int goal2AmountToReach = 25;
    public int goal3AmountToReach = 15;
    public int goal4AmountToReach = 1;
    public int goal5AmountToReach = 2;
    public int goal6AmountToReach = 50;

    [Header("Rest")]
    public bool goal4OnGoing = true;
    public PizzaCar pizzacar;

    private void Start()
    {
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    private void Update()
    {
        uim.goal1.text = goal1CurrentAmount + "  l  " + goal1AmountToReach;
        uim.goal2.text = goal2CurrentAmount + "  l  " + goal2AmountToReach;
        uim.goal3.text = goal3CurrentAmount + "  l  " + goal3AmountToReach;
        uim.goal4.text = goal4CurrentAmount + "  l  " + goal4AmountToReach;
        uim.goal5.text = goal5CurrentAmount + "  l  " + goal5AmountToReach;
        uim.goal6.text = goal6CurrentAmount + "  l  " + goal6AmountToReach;

        if (int.Parse(uim.scoreText.text) <= goal1AmountToReach)
        {
            goal1CurrentAmount = int.Parse(uim.scoreText.text);
        }
        else if (int.Parse(uim.scoreText.text) > goal1AmountToReach)
        {
            goal1CurrentAmount = goal1AmountToReach;
        }

        if (int.Parse(uim.scoreText.text) >= goal1AmountToReach && !pizzacar.repaired)
        {
            uim.orderText.text = "You've successfully delivered the pizza!" + "\n" + "You've also unlocked the car!" + "\n\n" + "Now get back to the pizzeria and go get the next one!";
            pizzacar.UnlockCar();
        }
    }

    public int AddToCurrentAmount(int currentAmount, int amountToReach)
    {
        if (currentAmount < amountToReach)
        {
            currentAmount++;
            return currentAmount;
        }
        else
        {
            return currentAmount;
        }
    }
}
