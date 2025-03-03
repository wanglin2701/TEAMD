using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceBar : MonoBehaviour
{
    //[SerializeField] GameObject customerPatienceBar; might need this container holder when applying it with customer objects who knows
    [SerializeField] Transform barAnchor;
    [SerializeField] SpriteRenderer barFill;
    [SerializeField] float customerPatience;
    private float customerMaxPatienceVar;
    private bool isStriked = false;
    private GameManager gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        customerMaxPatienceVar = customerPatience;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(gameObject); // Remove customer from scene
        }

    }
    void SetCustomerPatienceState(float customerCurrentPatience, float customerMaxPatience){
        float state = (float)customerCurrentPatience;
        state /= customerMaxPatience;
        if(state < 0){
            state = 0f;
        }
        barAnchor.transform.localScale = new Vector3(barAnchor.localScale.x, state, 1f);
        
        
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
}
