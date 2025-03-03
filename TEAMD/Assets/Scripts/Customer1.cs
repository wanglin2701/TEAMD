using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer1 : Customer
{
    public void Awake()
    {
        patienceMeterMax = 15f;
    }
    protected override void HandlePatience()
    {
        patienceMeter -= Time.deltaTime; // Faster depletion
        if (patienceMeter <= 0)
        {
            CustomerLeaves();
        }
    }
}
