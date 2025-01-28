using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isDragging = false;
    [SerializeField] private float flickForceMultiplier;
    [SerializeField] private float dragSpeed;
    [SerializeField] private float maxDragDistance;
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
}
