using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{   
    public static CustomerSpawner instance;
    public GameObject[] customers; //assign in inspector
    public int[] spawnPoints = new int[] {1, 2, 3};
    public Vector3[] spawnLoc; //assign in inspector
    Dictionary<int, GameObject> occupiedSeats = new Dictionary<int, GameObject>(); //CANNOT EXCEED 3
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), 3f, 50f);
    }
    

    public void SpawnCustomer()
    {
        if (occupiedSeats.Count >= 3) return;

        // Prioritize spawn order: 1 → 2 → 3
        List<int> availableSeats = new List<int>();
        if (!occupiedSeats.ContainsKey(1)) availableSeats.Add(1);
        if (!occupiedSeats.ContainsKey(2)) availableSeats.Add(2);
        if (!occupiedSeats.ContainsKey(3)) availableSeats.Add(3);

        if (availableSeats.Count == 0) return;

        // Always pick the lowest available seat number (1 first, then 2, then 3)
        int selectedSpawn = availableSeats[0];

        GameObject newCustomer = Instantiate(customers[UnityEngine.Random.Range(0, customers.Length)]);
        Customer customerScript = newCustomer.GetComponent<Customer>();
        customerScript.spawnPoint = selectedSpawn;

        customerScript.orderArray = OrderManager.instance.GenerateOrder(selectedSpawn).ToArray();

        List<Sprite> orderSprites = new List<Sprite>();
        foreach (GameObject ingredient in customerScript.orderArray)
        {
            if (ingredient != null) 
            {
                SpriteRenderer spriteRenderer = ingredient.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    orderSprites.Add(spriteRenderer.sprite);
                }
            }
        }

        // Now, it will always display OrderBubble1 first if possible
        OrderManager.instance.DisplayOrder(selectedSpawn, orderSprites);
        occupiedSeats.Add(selectedSpawn, newCustomer);
    }

    //call this when patience runs out or order is fulfilled
    public void DespawnCustomer(int spawn)
    {
        if(occupiedSeats.ContainsKey(spawn))
        {
            GameObject customer = occupiedSeats[spawn];

            //remove from dictionary
            occupiedSeats.Remove(spawn);

            //destroy everything
            Destroy(customer);
            OrderManager.instance.DestroyOrder(spawn);
        }
    }
}