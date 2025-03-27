using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    SoundManager soundManaager;
    public static CustomerSpawner instance;
    public GameObject[] customers; //assign in inspector
    public int[] spawnPoints = new int[] {1, 2, 3};
    public Vector3[] spawnLoc; //assign in inspector
    Dictionary<int, GameObject> occupiedSeats = new Dictionary<int, GameObject>(); //CANNOT EXCEED 3

    private int customerNumber;

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
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
        InvokeRepeating(nameof(SpawnCustomer), 3f, 10f);
    }
    

    public void SpawnCustomer()
    {
        Debug.Log(customers.Length);
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
                
                customerNumber = Random.Range(0, customers.Length);
                Debug.Log(customerNumber);
                //randomize customer selection
                GameObject newCustomer = Instantiate(customers[customerNumber], spawnLoc[spawn - 1], Quaternion.identity );

                //Play Alien Sound
                soundManaager.PlaySound("CustomerSpawn");

                StartCoroutine(DelaySpawnSound(1f));  //Alien moving up

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
    IEnumerator DelaySpawnSound(float seconds)
    {

        yield return new WaitForSeconds(seconds); // Wait for the given time
        switch (customerNumber)
        {
            case 0:
                soundManaager.PlaySound("RedAlien");
                break;

            case 1:
                soundManaager.PlaySound("BlueAlien");
                break;

            case 2:
                soundManaager.PlaySound("SmallAlien");
                break;

            case 3:
                soundManaager.PlaySound("BigBlueAlien");
                break;




        }

    }
}
