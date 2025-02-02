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
    [SerializeField] private float minVelocity;

    [Header("Floating")]
    public float floatStrength = 0.5f; // Controls how high the ingredient floats
    public float floatSpeed = 1.0f;   // Controls the speed of the floating motion
    public float floatDistance = 0.5f;  // Distance of the float
    public float moveSpeed = 2.0f;    // Speed of horizontal movement
    
    private Vector3 startPosition;
    private bool movingUp = true;     // Toggle for floating motion
    
    
    [Header("Movement & Physics")]
    public bool isMovingLeft = false;   // Flag for moving left
    public bool isMovingRight = true;   // Flag for moving right
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Floating Effect - Adjust Rigidbody2D velocity up and down
        if (movingUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, floatStrength);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -floatStrength / 2); // Make downward motion slower
        }

        // Toggle direction based on floating range
        if (transform.position.y > startPosition.y + floatDistance)
        {
            movingUp = false;
        }
        else if (transform.position.y < startPosition.y - floatDistance)
        {
            movingUp = true;
        }

    
        // Horizontal movement
        float moveDirection = 0;

        if (isMovingLeft)
        {
            moveDirection = -moveSpeed;
        }

        else if (isMovingRight)
        {
            moveDirection = moveSpeed;
        }

        // Apply horizontal movement
        rb.velocity = new Vector2(moveDirection, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    
        // Force
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click Detected - Applying Force!");
            AddForceUp();
        }
    }
    private void OnMouseDown(){
        startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }
    private void OnMouseUp(){
        if(isDragging){
            endTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 flickDirection = endTouchPosition - startTouchPosition;
            rb.AddForce(flickDirection * flickForceMultiplier, ForceMode2D.Impulse);

            isDragging = false;
        }
    }
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // Get the current mouse position
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the drag direction and clamp the distance
            Vector2 dragDirection = currentMousePosition - startTouchPosition;
            float dragDistance = Mathf.Min(dragDirection.magnitude, maxDragDistance); // Clamp to max distance

            // Calculate the clamped position
            Vector2 clampedPosition = startTouchPosition + dragDirection.normalized * dragDistance;

            // Move the object smoothly toward the clamped position
            rb.MovePosition(Vector2.Lerp(rb.position, clampedPosition, Time.deltaTime * dragSpeed));
        }
    }

    //Adding Ingrediant onto plate
    private void OnTriggerEnter2D(Collider2D collision)
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
        transform.SetParent(plate.transform);  //Ingredient become parent of the plate
        rb.velocity = Vector2.zero;
        AddedtoPlate = true;
        rb.isKinematic = true; // Prevent further physics interactions
        transform.localPosition = new Vector3(0f, 0f, 0f);  // You can adjust the x and y values for offset
        Debug.Log("Ingredient added to the plate!");
    }
    void AddForceUp()
        {
            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }


    // void OnMouseDown()
    // {
    //     Destroy(gameObject); // Destroys the object on click
    // }
}
