using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;
    [Header("Ingredients")]
    List<GameObject> order = new List<GameObject>();
    public GameObject[] ingredients; //assign in inspector

    [Header("Order Bubbles")]
    public GameObject orderBubble1; public GameObject orderBubble2; public GameObject orderBubble3; //assign in inspector
    public Vector3[] ingBubblePoints1; public Vector3[] ingBubblePoints2; public Vector3[] ingBubblePoints3;// assign in inspector
    List<GameObject> orderIngredients1 = new List<GameObject>(); List<GameObject> orderIngredients2 = new List<GameObject>(); List<GameObject> orderIngredients3 = new List<GameObject>();


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    public List<GameObject> GenerateOrder(int spawnPoint)
    {
        order.Clear();

        while (order.Count < 3) 
        {
            GameObject ingredient = ingredients[UnityEngine.Random.Range(0, ingredients.Length)]; // Explicitly using UnityEngine.Random
            order.Add(ingredient);
        }

        return order;
    }

    //called when customer is spawned, creates orders visually
    public void DisplayOrder(int spawn, List<Sprite> orderSprites)
    {
        int i = 0;
        switch(spawn)
        {
            case 1:
                Debug.Log("Displaying order at Bubble 1");
                orderBubble1.SetActive(true);
                foreach(Sprite sprite in orderSprites)
                {
                    GameObject ingredientImage = new GameObject("IngredientSprite");
                    SpriteRenderer renderer = ingredientImage.AddComponent<SpriteRenderer>();
                    renderer.sprite = sprite;
                    ingredientImage.transform.position = ingBubblePoints1[i];
                    i++;
                }
                break;

            case 2:
                Debug.Log("Displaying order at Bubble 2");
                orderBubble2.SetActive(true);
                foreach(Sprite sprite in orderSprites)
                {
                    GameObject ingredientImage = new GameObject("IngredientSprite");
                    SpriteRenderer renderer = ingredientImage.AddComponent<SpriteRenderer>();
                    renderer.sprite = sprite;
                    ingredientImage.transform.position = ingBubblePoints2[i];
                    i++;
                }
                break;

            case 3:
                Debug.Log("Displaying order at Bubble 3"); // Debugging if it reaches this case
                orderBubble3.SetActive(true);
                foreach(Sprite sprite in orderSprites)
                {
                    Debug.Log($"Ingredient {i} in Order Bubble 3: {sprite.name}"); // Debugging sprites
                    GameObject ingredientImage = new GameObject("IngredientSprite");
                    SpriteRenderer renderer = ingredientImage.AddComponent<SpriteRenderer>();
                    renderer.sprite = sprite;
                    ingredientImage.transform.position = ingBubblePoints3[i];
                    i++;
                }
                break;

            default:
                Debug.LogWarning($"Invalid spawn point {spawn} passed to DisplayOrder()");
                break;
        }
    }

    //called when customer despawns, deletes orders visually
    public void DestroyOrder(int spawn)
    {
        switch(spawn)
        {
            case 1:
            orderBubble1.SetActive(false);
            foreach(GameObject ingredient in orderIngredients1)
            {
                Destroy(ingredient);
            }
            orderIngredients1.Clear();
            break;

            case 2:
            orderBubble2.SetActive(false);
            foreach(GameObject ingredient in orderIngredients2)
            {
                Destroy(ingredient);
            }
            orderIngredients2.Clear();
            break;

            case 3:
            orderBubble3.SetActive(false);
            foreach(GameObject ingredient in orderIngredients3)
            {
                Destroy(ingredient);
            }
            orderIngredients3.Clear();
            break;
        }
    }
}