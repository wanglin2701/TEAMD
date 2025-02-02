using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPhysics : MonoBehaviour
{
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
    public Rigidbody2D rb;
    
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

        // Apply an initial floating force
        rb.velocity = new Vector2(0, floatStrength);
    }

    
    void Update()
    {
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
    
        // Force
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click Detected - Applying Force!");
            AddForceUp();
        }
    }


        void AddForceUp()
        {
            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }


        void OnMouseDown()
        {
            Destroy(gameObject); // Destroys the object on click
        }

}
