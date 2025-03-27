using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer4 : Customer
{
    public void Awake()
    {
        patienceMeterMax = 30f;
        scoreReward = 50;   
    }
    protected override void HandlePatience()
    {
        patienceMeter -= Time.deltaTime * patienceDepletionRate; // Faster depletion
        if (patienceMeter <= 0)
        {
            CustomerLeavesAngrily();

        }
    }
}
