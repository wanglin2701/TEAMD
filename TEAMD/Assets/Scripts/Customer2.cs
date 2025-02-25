using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//order changer
public class Customer2 : Customer
{
    private float orderChangeTime;

    protected override void Start()
    {
        base.Start();
        orderChangeTime = patienceMeterMax / 2f; // Change order midway
    }

    protected override void Update()
    {
        base.Update();
        if (patienceMeter <= orderChangeTime)
        {
            ChangeOrder();
        }
    }

    private void ChangeOrder()
    {
       // orderArray = OrderManager.GenerateOrder(); // Generate a new order
    }
}
