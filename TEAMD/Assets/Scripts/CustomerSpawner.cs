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
        InvokeRepeating(nameof(SpawnCustomer), 3f, 10f);
    }
    

    public void SpawnCustomer()
    {
        //if enough customers at table, do not spawn
        if(occupiedSeats.Count >= 3)
        {
            return;
        }

        //check empty seats
        foreach(int spawn in spawnPoints)
        {
            if(!occupiedSeats.ContainsKey(spawn))
            {
                //randomize customer selection
                GameObject newCustomer = Instantiate(customers[Random.Range(0, customers.Length)], spawnLoc[spawn - 1], Quaternion.identity );

                //assign spawn point to customer
                Customer customerScript = newCustomer.GetComponent<Customer>();
                customerScript.spawnPoint = spawn;

                //generate & display order
                customerScript.orderArray = OrderManager.instance.GenerateOrder(spawn).ToArray<GameObject>();
                OrderManager.instance.DisplayOrder(spawn, customerScript.orderArray.ToList<GameObject>());

                //add customer to occupied dictionary
                occupiedSeats.Add(spawn, newCustomer);
                break;
            }
        }
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
