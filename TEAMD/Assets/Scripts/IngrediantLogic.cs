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
    [SerializeField] private float dragSpeed;
    [SerializeField] private float maxDragDistance;

    private bool AddedtoPlate;  
    private float minVelocity = 4f;

    [Header("Floating")]
    public float floatStrength = 0.5f; // Controls how high the ingredient floats
    public float floatSpeed = 1.0f;   // Controls the speed of the floating motion
    public float floatDistance = 0.5f;  // Distance of the float
    public float moveSpeed = 2.0f;    // Speed of horizontal movement
    
    private Vector3 startPosition;
    private bool movingUp = true;     // Toggle for floating motion
    public GameObject targetPlate;
    public float offsetRange = 2f;
    public float directionVariation = 10f;
    private Vector2 targetOffset;

    private Plate plate_Script;

    private Vector3 offset;

    private Vector3 lastMousePosition;
    private Vector3 velocity;
    private Vector3 startDragPosition; // Stores the starting position when clicked
    [SerializeField] private float maxDragRadius = 2f; // Adjust this for desired movement radius

    
    // Start is called before the first frame update
    void Start()
    {
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
        isDragging = true;
        rb.isKinematic = true; // Temporarily disable physics
        startDragPosition = transform.position; // Save initial click position
        lastMousePosition = GetMouseWorldPos();
        offset = transform.position - lastMousePosition;

        // if(AddedtoPlate == false)
        // {
        //     startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     isDragging = true;
        // }
        
    }
    private void OnMouseUp(){
        isDragging = false;
        rb.isKinematic = false; // Re-enable physics
        velocity = (GetMouseWorldPos() - lastMousePosition) / Time.deltaTime;
        rb.velocity = velocity * flickForceMultiplier;
        // if(isDragging){
        //     endTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //     Vector2 flickDirection = endTouchPosition - startTouchPosition;
        //     rb.AddForce(flickDirection * flickForceMultiplier, ForceMode2D.Impulse);

        //     isDragging = false;
        // }
    }
    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 targetPosition = GetMouseWorldPos() + offset;

        // 🔹 Apply movement limit based on radius
        float distanceFromStart = Vector3.Distance(startDragPosition, targetPosition);
        if (distanceFromStart > maxDragRadius)
        {
            // Clamp position within max radius
            targetPosition = startDragPosition + (targetPosition - startDragPosition).normalized * maxDragRadius;
        }

        rb.position = targetPosition; // Directly update position
        // if (isDragging)
        // {
        //     // Get the current mouse position
        //     Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //     // Calculate the drag direction and clamp the distance
        //     Vector2 dragDirection = currentMousePosition - startTouchPosition;
        //     float dragDistance = Mathf.Min(dragDirection.magnitude, maxDragDistance); // Clamp to max distance

        //     // Calculate the clamped position
        //     Vector2 clampedPosition = startTouchPosition + dragDirection.normalized * dragDistance;

        //     // Move the object smoothly toward the clamped position
        //     rb.MovePosition(Vector2.Lerp(rb.position, clampedPosition, Time.deltaTime * dragSpeed));
        // }
    }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 10f; // Set a fixed depth value, adjust as needed
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    //Adding Ingrediant onto plate
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the plate
        if (collision.gameObject.CompareTag("Plate"))
        {
            Debug.Log(rb.velocity);

            // Check if the velocity is higher than the minimum
            if (rb.velocity.magnitude > minVelocity)
            {
                AddToPlate(collision.gameObject);
            }

            else
            {
                Debug.Log("Velocity too low to add to plate.");
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
