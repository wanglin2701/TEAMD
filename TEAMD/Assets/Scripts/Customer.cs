using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base for normal customer
public class Customer : MonoBehaviour
{
    public int spawnPoint;
    public enum BehaviourType { Impatient, OrderChanger, VIP }

    [Header("Customer Settings")]
    public float patienceMeterMax; // Max patience
    protected float patienceMeter;    // Current patience
    public const float patienceDepletionRate = 1f; // Depletion rate 
    public int scoreReward;        // Score given upon order completion
    public BehaviourType behaviourType;

    [Header("Order System")]
    public GameObject[] orderArray; // Array for storing the customer's order

    private bool isServed = false;

    protected virtual void Start()
    {
        patienceMeter = patienceMeterMax;
    }

    protected virtual void Update()
    {
        HandlePatience();
    }

    protected virtual void HandlePatience()
    {
        if (!isServed)
        {
            patienceMeter -= patienceDepletionRate * Time.deltaTime;
            if (patienceMeter <= 0)
            {
                CustomerLeaves();
            }
        }
    }

    protected virtual void GenerateOrder()
    {
       //orderArray = OrderManager.instance.GenerateOrder(); // call from order manager script
    }

    public virtual void ServeOrder(GameObject[] playerOrder)
    {
        if (OrderMatches(playerOrder))
        {
            isServed = true;
            GameManager.instance.AddScore(scoreReward); // call from gameManager script
            CustomerLeaves();
        }
    }

    protected virtual bool OrderMatches(GameObject[] playerOrder)
    {
        if (playerOrder.Length != orderArray.Length) return false;

        for (int i = 0; i < orderArray.Length; i++)
        {
            if (playerOrder[i] != orderArray[i]) return false;
        }

        return true;
    }

    protected virtual void CustomerLeaves()
    {
        Destroy(gameObject); // Customer leaves
        CustomerSpawner.instance.DespawnCustomer(spawnPoint);
    }
}
