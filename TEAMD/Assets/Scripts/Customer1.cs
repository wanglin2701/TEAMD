using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer1 : Customer
{
    protected override void HandlePatience()
    {
        patienceMeter -= (patienceDepletionRate * 2f) * Time.deltaTime; // Faster depletion
        if (patienceMeter <= 0)
        {
            CustomerLeaves();
        }
    }
}
