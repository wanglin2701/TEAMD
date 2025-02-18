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
    public GameObject targetPlate;
    public float offsetRange = 2f;
    public float directionVariation = 10f;
    private Vector2 targetOffset;
    
    
    [Header("Movement & Physics")]
    public bool isMovingLeft = false;   // Flag for moving left
    public bool isMovingRight = true;   // Flag for moving right
    
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

    // Update is called once per frame
    void Update()
    {

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
        transform.SetParent(plate.transform);  //Ingredient become parent of the plate
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f; // Stops the ingrediants from spinning when adding into the plate
        //transform.rotation = Quaternion.Euler(0f,0f,0f); // <-- Makes the ingrediants add into the plate upright, but the weird rotations looks quite cartoonish IMO
        AddedtoPlate = true;
        rb.isKinematic = true; // Prevent further physics interactions
        transform.localPosition = new Vector3(0f, 0f, 0f);  // You can adjust the x and y values for offset
        Debug.Log("Ingredient added to the plate!");
    }
    void AddForceUp()
        {
            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }

}
