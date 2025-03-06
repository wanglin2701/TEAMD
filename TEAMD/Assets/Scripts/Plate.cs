using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Plate : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool isDragging = false;
    private float x_margin = 0.105f; // Margin percentage to reduce the camera bounds
    private float y_margin = 0.19f; // Margin percentage to reduce the camera bounds
    private bool isTriggering = false;

    private int maxPlateLoad = 3;

    [SerializeField] private GameObject[] PlateInventory;

    private GameManager gameManager;


    void Start()
    {
        cam = Camera.main; // Get the main camera
        PlateInventory = new GameObject[maxPlateLoad]; //create the array with a max size
        gameManager = FindObjectOfType<GameManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the collided object is the plate
        if (collision.gameObject.CompareTag("Bin")  && isTriggering == false)
        {
            isTriggering = true;
            Debug.Log("Clear Plate");
            ClearPlate();
        }

        // Check if the plate collides with a customer
        if (collision.gameObject.CompareTag("Customer") && isTriggering == false)
        {
            isTriggering = true;

            Debug.Log("Plate touched the customer");
            
            // Serve the order to the customer
            Customer customer = collision.gameObject.GetComponent<Customer>();
            if (customer != null)
            {
                // Pass the plate ingredients to the customer to check if the order is correct
                customer.ServeOrder(GetIngredients());
                ClearPlate(); // Clear the ingredients from the plate
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggering = false;
    }


    #region Plate Controls
    void OnMouseDown()
    {
        if(gameManager.isPaused == false)
        {
            offset = transform.position - GetMouseWorldPosition();
            isDragging = true;
        }
     
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)) + offset;

            // Get the original camera bounds in world space
            float cameraLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float cameraRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float cameraBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float cameraTop = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            // Reduce the camera bounds by the margin percentage
            float widthMargin = (cameraRight - cameraLeft) * x_margin;
            float heightMargin = (cameraTop - cameraBottom) * y_margin;

            // Clamp the new position to the reduced camera bounds
            newPosition.x = Mathf.Clamp(newPosition.x, cameraLeft + widthMargin, cameraRight - widthMargin);
            newPosition.y = Mathf.Clamp(newPosition.y, cameraBottom + heightMargin, cameraTop - heightMargin);

            transform.position = newPosition;

        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0f; // Ensure 2D plane
        return cam.ScreenToWorldPoint(mousePoint);
    }
    #endregion

    public int CheckPlateLoad()
    {
        for(int i = 0; i < PlateInventory.Length; i++)
        {
            if(PlateInventory[i] == null)
            {
                return i;
            }
        }
        return 707; //Considered null 
    }

    public void AddIngredienttoPlate(GameObject ingredient, int position)
    {
        Transform slot = transform.GetChild(position); // Get child at the given index
        ingredient.transform.SetParent(slot); // Set ingredient as a child of the slot
        ingredient.transform.localPosition = Vector3.zero; // Reset position inside the slot

        ingredient.transform.position = new Vector3(0, 0, 0); // Set new position
        ingredient.transform.localScale = new Vector3(0.74f, 0.74f, 0.74f);  //set new scale 

        PlateInventory[position] = ingredient;
    }
    public void ClearPlate()
    {

        for (int i = 0; i < PlateInventory.Length; i++)
        {
            if (PlateInventory[i] != null)
            {
                GameObject ingredient = PlateInventory[i];
                Destroy(ingredient);
                PlateInventory[i] = null;

            }
        }
    }

    public GameObject[] GetIngredients()
    {
        List<GameObject> ingredients = new List<GameObject>();
        for (int i = 0; i < PlateInventory.Length; i++)
        {
            if (PlateInventory[i] != null)
            {
                ingredients.Add(PlateInventory[i]);
            }
        }
        return ingredients.ToArray();
    }

    

}
