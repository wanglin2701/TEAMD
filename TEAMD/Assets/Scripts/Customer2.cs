using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//order changer
public class Customer2 : Customer
{
    private float orderChangeTime;
    private bool hasChangedOrder;

    public void Awake()
    {
        hasChangedOrder = false;
        patienceMeterMax = 30f;
        scoreReward = 150;
        orderChangeTime = patienceMeterMax / 2f; // Change order midway
    }

    protected override void HandlePatience()
    {
        patienceMeter -= Time.deltaTime * patienceDepletionRate; // Faster depletion
        if (!hasChangedOrder && patienceMeter <= orderChangeTime)
        { 
            
            ChangeOrder();
            hasChangedOrder = true;
        }
        if (patienceMeter <= 0)
        {
            CustomerLeavesAngrily();

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
