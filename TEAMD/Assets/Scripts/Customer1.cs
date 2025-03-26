using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//impatient
public class Customer1 : Customer
{

    public void Awake()
    {
        patienceMeterMax = 30f;
        scoreReward = 100;   
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
