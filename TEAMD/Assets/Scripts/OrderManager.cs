using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    [Header("Order Bubbles")]
    public GameObject orderBubble1; public GameObject orderBubble2; public GameObject orderBubble3; //assign in inspector
    public Vector3[] ingBubblePoints1; public Vector3[] ingBubblePoints2; public Vector3[] ingBubblePoints3;// assign in inspector

    [Header("Order Trackers")]
    List<GameObject> orderIngredients1 = new List<GameObject>();
    List<GameObject> orderIngredients2 = new List<GameObject>();
    List<GameObject> orderIngredients3 = new List<GameObject>();
    List<string> order1IDs = new List<string>();
    List<string> order2IDs = new List<string>();
    List<string> order3IDs = new List<string>();
    List<string> ingredientsOrdered = new List<string>();


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        orderBubble1.SetActive(false);
        orderBubble2.SetActive(false);
        orderBubble3.SetActive(false);
    }

    public List<GameObject> GenerateOrder(int spawnPoint)
    {
        List<GameObject> order = new List<GameObject>();
        List<string> validRecipes = new List<string>();

        //sort through which difficulty recipes according to the time
        foreach(Recipe recipe in DataManager.instance.recipeData.Values)
        {
            int ingredientCount = recipe.ParseIngredients().Count;

            //game in 3-2 minute countdown, still easy mode
            if(GameManager.instance.levelTime <= 181 && GameManager.instance.levelTime > 120)
            {
                if(ingredientCount == 1)
                {
                    validRecipes.Add(recipe.id);
                }
            }
            //game in 2-1 minute countdown, medium mode
            else if (GameManager.instance.levelTime <= 120 && GameManager.instance.levelTime > 60)
            {
                if(ingredientCount == 2)
                {
                    validRecipes.Add(recipe.id);
                }
            }
            else //game less than 60 seconds left, hard mode
            {
                if(ingredientCount== 3)
                {
                    validRecipes.Add(recipe.id);
                }
            }
        }

        //select random recipe from dictionary
        string randomRecipeKey = validRecipes[Random.Range(0, validRecipes.Count)];
        Recipe selectedRecipe = DataManager.instance.recipeData[randomRecipeKey];

        //retrieve ingredient IDs from recipe
        List<string> ingredientIDs = selectedRecipe.ParseIngredients();

        foreach(string id in ingredientIDs)
        {
            if(DataManager.instance.ingredientData.TryGetValue(id, out Ingredient ingredientInfo))
            {
                if(DataManager.instance.ingredientPrefabs.TryGetValue(ingredientInfo.name, out GameObject ingredientPrefab))
                {
                    order.Add(ingredientPrefab);
                }
            }
        }
        return order;
    }
    
    //for use in ingredient algorithm in ingredientSpawner.cs
    public List<string> ObserveOrders()
    {
        ingredientsOrdered.Clear();

        foreach(string id in order1IDs)
        {
            ingredientsOrdered.Add(id);
        }

        foreach(string id in order2IDs)
        {
            ingredientsOrdered.Add(id);
        }

        foreach(string id in order3IDs)
        {
            ingredientsOrdered.Add(id);
        }
        return ingredientsOrdered;
    }

    //called when customer is spawned, creates orders visually
    public void DisplayOrder(int spawn, List<GameObject> order)
    {
        int i = 0;

        switch(spawn)
        {
            case 1:
            orderBubble1.SetActive(true);
            order1IDs.Clear();
            foreach(GameObject ingredient in order)
            {
                GameObject ingreDisplay = Instantiate(ingredient, ingBubblePoints1[i], Quaternion.identity);
                RemoveIngredientStuff(ingreDisplay);
                ingreDisplay.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                orderIngredients1.Add(ingreDisplay);

                string ingredientName = ingredient.name.Replace("(Clone)", "");
                string id = DataManager.instance.GetIngredientIDByName(ingredientName);
                order1IDs.Add(id);

                i++;
            }
            break;

            case 2:
            orderBubble2.SetActive(true);
            order2IDs.Clear();
            foreach(GameObject ingredient in order)
            {
                GameObject ingreDisplay = Instantiate(ingredient, ingBubblePoints2[i], Quaternion.identity);
                RemoveIngredientStuff(ingreDisplay);
                ingreDisplay.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                orderIngredients2.Add(ingreDisplay);

                string ingredientName = ingredient.name.Replace("(Clone)", "");
                string id = DataManager.instance.GetIngredientIDByName(ingredientName);
                order2IDs.Add(id);

                i++;
            }
            break;

            case 3:
            orderBubble3.SetActive(true);
            order3IDs.Clear();
            foreach(GameObject ingredient in order)
            {
                GameObject ingreDisplay = Instantiate(ingredient, ingBubblePoints3[i], Quaternion.identity);
                RemoveIngredientStuff(ingreDisplay);
                ingreDisplay.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                orderIngredients3.Add(ingreDisplay);

                string ingredientName = ingredient.name.Replace("(Clone)", "");
                string id = DataManager.instance.GetIngredientIDByName(ingredientName);
                order3IDs.Add(id);

                i++;
            }
            break;
        }
    }

    void RemoveIngredientStuff(GameObject ingredient)
    {
        Rigidbody2D rb = ingredient.GetComponent<Rigidbody2D>();
        Collider2D collider = ingredient.GetComponent<Collider2D>();
        IngrediantLogic script = ingredient.GetComponent<IngrediantLogic>();
        Destroy(rb);
        Destroy(collider);
        Destroy(script);
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