using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediantLogic : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isDragging = false;
    [SerializeField] private float flickForceMultiplier;
    [SerializeField] private float maxDragRadius; // Adjust this for desired movement radius

    private bool AddedtoPlate;  
    private float minVelocity = 15f;

    [Header("Floating")]
    public float moveSpeed = 2.0f;    // Speed of horizontal movement
    
    public GameObject targetPlate;
    public float offsetRange = 2f;
    public float directionVariation = 10f;
    private Vector2 targetOffset;

    private Plate plate_Script;

    private Vector3 offset;

    private Vector3 lastMousePosition;
    private Vector3 velocity;
    private Vector3 startDragPosition; // Stores the starting position when clicked


    public string IngredientType;

    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        rb = GetComponent<Rigidbody2D>();
        targetPlate = GameObject.Find("Plate");

        if (targetPlate != null)
        {
            Vector2 targetPos = targetPlate.transform.position;

            // Pick a random offset near the target
            targetOffset = new Vector2(
                targetPos.x + Random.Range(-offsetRange, offsetRange),
                targetPos.y + Random.Range(-offsetRange, offsetRange)
            );

            // Compute initial direction
            Vector2 direction = (targetOffset - (Vector2)transform.position).normalized;

            // Add a small random angle variation
            float randomAngle = Random.Range(-directionVariation, directionVariation);
            direction = Quaternion.Euler(0, 0, randomAngle) * direction;

            // Apply movement
            rb.velocity = direction * moveSpeed;
        }
    }
    void Update()
    {
        if (isDragging)
        {
            lastMousePosition = GetMouseWorldPos();
        }
    }
    private void OnMouseDown(){

        if(!AddedtoPlate && gameManager.isPaused == false)
        {
            isDragging = true;
            rb.isKinematic = true;
            startDragPosition = transform.position;
            lastMousePosition = GetMouseWorldPos();
            offset = transform.position - lastMousePosition;
        }
    }

    private void OnMouseUp(){
     

        if (!AddedtoPlate && gameManager.isPaused == false)
        {
            isDragging = false;
            rb.isKinematic = false; // Re-enable physics
            velocity = (GetMouseWorldPos() - lastMousePosition) / Time.deltaTime;
            rb.velocity = velocity * flickForceMultiplier;
        }

        if (targetPlate != null && !AddedtoPlate)
        {
            Collider2D plateCollider = targetPlate.GetComponent<Collider2D>();

            if (plateCollider.OverlapPoint(transform.position))
            {
                Debug.Log("Ingredient is over the plate!");
                AddToPlate(targetPlate);
                return; // Skip the velocity check
            }
        }
    }

    private void OnMouseDrag()
    {
        if (!isDragging || AddedtoPlate) return; // Prevent movement when added to plate
        if(!AddedtoPlate){
            Vector3 targetPosition = GetMouseWorldPos() + offset;

            // Apply movement limit based on radius
            float distanceFromStart = Vector3.Distance(startDragPosition, targetPosition);
            if (distanceFromStart > maxDragRadius)
            {
                // Clamp position within max radius
                targetPosition = startDragPosition + (targetPosition - startDragPosition).normalized * maxDragRadius;
            }

            rb.position = targetPosition; // Directly update position
        }
        
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 10f; // Set a fixed depth value, adjust as needed
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    //Adding Ingrediant onto plate
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the plate
        if (collision.gameObject.CompareTag("Plate"))
        {

            // Check if the velocity is higher than the minimum
            if (rb.velocity.magnitude > minVelocity)
            {
                AddToPlate(collision.gameObject);
            }

            else
            {
                //Debug.Log("Velocity too low to add to plate.");
            }
        }
    }

    private void AddToPlate(GameObject plate)
    {
        plate_Script = plate.GetComponent<Plate>(); //Retrieve plate script

        //Add Ingredient to the plate game object
        int ingredientPosition = plate_Script.CheckPlateLoad();

        if(ingredientPosition != 707)
        {
            //Update the plate inventory
            plate_Script.AddIngredienttoPlate(gameObject, ingredientPosition);  //Add current gameobject(ingredient) to script
            
            //Physics Polishing when in Plate
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f; // Stops the ingrediants from spinning when adding into the plate
                                     //transform.rotation = Quaternion.Euler(0f,0f,0f); // <-- Makes the ingrediants add into the plate upright, but the weird rotations looks quite cartoonish IMO
            AddedtoPlate = true;
            rb.isKinematic = true; // Prevent further physics interactions
            transform.localPosition = new Vector3(0f, 0f, 0f);  // You can adjust the x and y values for offset
            Debug.Log("Ingredient added to the plate!");
        }

        else
        {
            Debug.Log("Plate Full!!");
        }

        
    }
    void AddForceUp()
        {
            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }

}
