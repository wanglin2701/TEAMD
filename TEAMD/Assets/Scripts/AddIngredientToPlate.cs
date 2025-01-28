using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IngredientPlateCollision : MonoBehaviour
{
    public float minVelocity = 5f; // Minimum speed to add to plate
    private Rigidbody2D rb;
    public float speed; // Speed of the ingredient
    public Vector2 direction;
    private bool AddedtoPlate;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //Get ingredient rigidbody2d   
    }

    private void FixedUpdate()
    {
        // if (AddedtoPlate == false)
        // {
        //     rb.velocity = direction * speed;

        // }
        //Debug.Log(rb.velocity);


    }
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
