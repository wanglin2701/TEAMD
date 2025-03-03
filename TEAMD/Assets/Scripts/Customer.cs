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
        if (!isStriked)
        {
            customerPatience -= Time.deltaTime;
            SetCustomerPatienceState(customerPatience, customerMaxPatienceVar);
            SetPatienceBarColor();    
        }

        if (customerPatience <= 0 && !isStriked)
        {
            isStriked = true;
            gameManager.AddStrike(); // Add a strike when patience is 0
            DestroyPatienceBar(); 
            Destroy(gameObject); // Remove customer from scene
        }
    }

    #region PATIENCE BAR

     private void DestroyPatienceBar()
    {
        if (patienceBarInstance != null)
        {
            Destroy(patienceBarInstance);
        }
    }

    void SetCustomerPatienceState(float customerCurrentPatience, float customerMaxPatience){
        float state = (float)customerCurrentPatience;
        state /= customerMaxPatience;
        if(state < 0){
            state = 0f;
        }
        bar.transform.localScale = new Vector3(bar.localScale.x, state, 1f);
        
        
    }
    void SetPatienceBarColor(){
        float yellowPatience = customerMaxPatienceVar * 0.6f;
        float redPatience = customerMaxPatienceVar * 0.25f;
        //Debug.Log(customerPatience);
        if(customerPatience <= redPatience){
            barFill.color = Color.red;

        } else if(customerPatience <= yellowPatience){
            barFill.color = Color.yellow;

        }
    }

    #endregion

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Plate plate = collision.GetComponent<Plate>();

        if (plate != null)
        {
            ServeOrder(plate.GetIngredients()); 
            Destroy(collision.gameObject); 
        }
    }

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
            gameManager.AddStrike(); 
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
#endregion
