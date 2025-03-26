using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//order changer
public class Customer2 : Customer
{
    private float orderChangeTime;
    private bool hasChangedOrder = false;

    protected override void Start()
    {
        base.Start();
        orderChangeTime = patienceMeterMax / 2f; // Change order midway
    }

    protected override void Update()
    {
        base.Update();

         // If patience reaches half and the order hasn't changed yet, change it
        if (!hasChangedOrder && patienceMeter <= orderChangeTime)
        {
            ChangeOrder();
            hasChangedOrder = true;
        }
    }

    private void ChangeOrder()
    {
       // Destroy current order visually
        OrderManager.instance.DestroyOrder(spawnPoint);

        // Generate a new order
        List<GameObject> newOrder = OrderManager.instance.GenerateOrder(spawnPoint);
        orderArray = newOrder.ToArray();

        // Display the new order visually
        OrderManager.instance.DisplayOrder(spawnPoint, newOrder);

        Debug.Log("Customer changed order!");
    }
}
