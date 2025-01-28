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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
