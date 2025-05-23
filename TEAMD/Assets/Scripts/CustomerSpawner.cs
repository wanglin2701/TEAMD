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
    public float slideDuration; //assign in inspector
    public float spawnrate = 6f;
    public float spawnDelay; //assign in inspector
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
        StartCoroutine(SpawnCustomerRoutine());
    }

    void Update()
    {
        //adjust spawnrate based on time in level in GameManager.cs
        if(GameManager.instance.levelTime <= 150 && GameManager.instance.levelTime > 120)
        {
            spawnrate = 4f;
        }
        else if(GameManager.instance.levelTime <= 120 && GameManager.instance.levelTime > 90)
        {
            spawnrate = 2f;
        }
        else if(GameManager.instance.levelTime <= 90 && GameManager.instance.levelTime > 60)
        {
            spawnrate = 1f;
        }
        else if(GameManager.instance.levelTime < 60)
        {
            spawnrate = 0.5f;
        }
    }

    IEnumerator SpawnCustomerRoutine()
    {
        while(true)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(spawnrate);
        }
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

                //find positions and randomize customer selection
                Vector3 loweredPosition = new Vector3(spawnLoc[spawn - 1].x, spawnLoc[spawn -1].y - spawnDelay, 0);
                Vector3 hightenedPosition = spawnLoc[spawn - 1];
                GameObject newCustomer = Instantiate(customers[customerNumber], loweredPosition, Quaternion.identity);

                //slide customer up
                newCustomer.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(SlideCustomer(newCustomer.transform, loweredPosition, hightenedPosition, spawn));

                //Play Alien Sound
                soundManaager.PlaySound("CustomerSpawn");

                StartCoroutine(DelaySpawnSound(1f));  //Alien moving up

                //add customer to occupied dictionary
                occupiedSeats.Add(spawn, newCustomer);
                break;
            }
        }
    }

    IEnumerator SlideCustomer(Transform customerTransform, Vector3 loweredPos, Vector3 hightenedPos, int spawn)
    {
        float elapsedTime = 0f;
        
        //animation for sliding
        while(elapsedTime < slideDuration)
        {
            customerTransform.position = Vector3.Lerp(loweredPos, hightenedPos, elapsedTime/slideDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        customerTransform.position = hightenedPos;

        //allowed to serve customer now
        customerTransform.GetComponent<Collider2D>().enabled = true;
        
        //assign spawn point to customer
        Customer customerScript = customerTransform.GetComponent<Customer>();
        customerScript.spawnPoint = spawn;

        //generate & display order
        customerScript.orderArray = OrderManager.instance.GenerateOrder(spawn).ToArray<GameObject>();
        OrderManager.instance.DisplayOrder(spawn, customerScript.orderArray.ToList<GameObject>());
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
