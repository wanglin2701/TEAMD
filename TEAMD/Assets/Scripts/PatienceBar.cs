using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceBar : MonoBehaviour
{
    //[SerializeField] GameObject customerPatienceBar; might need this container holder when applying it with customer objects who knows
    [SerializeField] Transform bar;
    [SerializeField] Image barFill;
    [SerializeField] float customerPatience;
    private float customerMaxPatienceVar;
    
    // Start is called before the first frame update
    void Start()
    {
        customerMaxPatienceVar = customerPatience;
    }

    // Update is called once per frame
    void Update()
    {
        customerPatience -= Time.deltaTime;
        SetCustomerPatienceState(customerPatience, customerMaxPatienceVar);
        SetPatienceBarColor();
    }
    public void SetCustomerPatienceState(float customerCurrentPatience, float customerMaxPatience){
        float state = (float)customerCurrentPatience;
        state /= customerMaxPatience;
        if(state < 0){
            state = 0f;
        }
        bar.transform.localScale = new Vector3(bar.localScale.x, state, 1f);
        
        
    }
    public void SetPatienceBarColor(){
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
