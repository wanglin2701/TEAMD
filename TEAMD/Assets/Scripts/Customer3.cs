using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//vip customer
public class Customer3 : Customer
{
   protected override void Start()
    {
        base.Start();
        scoreReward *= 2; // VIP customers give more score
    }

    
}
