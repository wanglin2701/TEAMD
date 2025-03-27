using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//vip customer
public class Customer3 : Customer
{
   public void Awake()
    {
        patienceMeterMax = 15f;   
        scoreReward = 200; // VIP customers give more score
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
