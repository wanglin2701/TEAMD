using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Patience Bar")]
    private GameObject patienceBarInstance;
    [SerializeField] Transform bar;
    [SerializeField] Image barFill;


    [SerializeField] float customerPatience;
    private float customerMaxPatienceVar;
    private bool isStriked = false;
    private GameManager gameManager;

    protected virtual void Start()
    {
        patienceMeter = patienceMeterMax;
        customerMaxPatienceVar = customerPatience;
        gameManager = FindObjectOfType<GameManager>();
        //SpawnPatienceBar();
    }

    protected virtual void Update()
    {
        HandlePatience();
        //if (!isStriked)
        //{
        //    customerPatience -= Time.deltaTime;
        //    SetCustomerPatienceState(customerPatience, customerMaxPatienceVar);
        //    SetPatienceBarColor();    
        //}

        //if (customerPatience <= 0 && !isStriked)
        //{
        //    isStriked = true;
        //    gameManager.AddStrike(); // Add a strike when patience is 0
        //    DestroyPatienceBar(); 
        //    Destroy(gameObject); // Remove customer from scene
        //}
    }

    //#region PATIENCE BAR

    // private void DestroyPatienceBar()
    //{
    //    if (patienceBarInstance != null)
    //    {
    //        Destroy(patienceBarInstance);
    //    }
    //}

    //void SetCustomerPatienceState(float customerCurrentPatience, float customerMaxPatience){
    //    float state = (float)customerCurrentPatience;
    //    state /= customerMaxPatience;
    //    if(state < 0){
    //        state = 0f;
    //    }
    //    bar.transform.localScale = new Vector3(bar.localScale.x, state, 1f);


    //}
    //void SetPatienceBarColor(){
    //    float yellowPatience = customerMaxPatienceVar * 0.6f;
    //    float redPatience = customerMaxPatienceVar * 0.25f;
    //    //Debug.Log(customerPatience);
    //    if(customerPatience <= redPatience){
    //        barFill.color = Color.red;

    //    } else if(customerPatience <= yellowPatience){
    //        barFill.color = Color.yellow;

    //    }
    //}

    //#endregion

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

    #region SERVE ORDER

    // Serve order when a plate is dragged onto the customer
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("HELLO");

    //    Plate plate = collision.GetComponent<Plate>();

    //    if (plate != null)
    //    {

    //        ServeOrder(plate.GetIngredients()); 
    //        Destroy(collision.gameObject); 
    //    }
    //}

    public virtual void ServeOrder(GameObject[] playerOrder)
    {


        if (OrderMatches(playerOrder))
        {
            isServed = true;
            GameManager.instance.AddScore(scoreReward); // call from gameManager script
            CustomerLeaves();
        }

        else 
        {
            Debug.Log("hello");
            gameManager.AddStrike();
            CustomerLeaves(); 
        }
    }

    protected virtual bool OrderMatches(GameObject[] playerOrder)
    {

        // Check if the lengths are equal first
        if (playerOrder.Length != orderArray.Length)
            return false; // If lengths don't match, orders can't be the same

        //Retrieve the string from the Plate Inventory (Get all the ingredient type in the plate)
        List<string> plateInventoryString = new List<string>();
        foreach (GameObject playerOrderItem in playerOrder)
        {
            plateInventoryString.Add(playerOrderItem.GetComponent<IngrediantLogic>().IngredientType);
        }

        List<string> customerOrderString = new List<string>();
        foreach (GameObject customerItem in orderArray)
        {
            customerOrderString.Add(customerItem.GetComponent<IngrediantLogic>().IngredientType);
        }

        //Check for values
        //Debug.Log("Player order list: " + plateInventoryString[0]);
        //Debug.Log("Order order list: " + customerOrderString[0]);

        //if (plateInventoryString[0] == customerOrderString[0])
        //{
        //    Debug.Log("Ingredients are the same!");
        //}

        //else
        //{
        //    Debug.Log("Ingredients are wrong!");
        //}

        // Check if every ingredient in the playerOrder exists in orderList
        foreach (var ingredient in plateInventoryString)
        {
            // If the ingredient exists in orderList, remove it (accounting for one occurrence)
            if (customerOrderString.Contains(ingredient))
            {
                Debug.Log(ingredient + "is inside order");
                customerOrderString.Remove(ingredient);
            }
            else
            {
                Debug.Log(ingredient + "does not contain");
                // If any ingredient from playerOrder is not in orderList, return false
                return false;
            }
        }

        // If orderList is empty, it means all ingredients have been matched correctly
        return true; //return true if the orderList is empty
    }

    protected virtual void CustomerLeaves()
    {
        Destroy(gameObject); // Customer leaves
        CustomerSpawner.instance.DespawnCustomer(spawnPoint);
    }

    public virtual void CustomerLeavesAngrily()
    {
        Destroy(gameObject); // Customer leaves
        CustomerSpawner.instance.DespawnCustomer(spawnPoint);
        gameManager.AddStrike();

    }
}
#endregion
