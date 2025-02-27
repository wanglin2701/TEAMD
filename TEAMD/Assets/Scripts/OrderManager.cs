using System;
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
        //clear order from before
        order.Clear();

        //max amount of ingredients in an order is 3
        for(int i = 0; i < 3; i++)
        {
            //based on luck, add nothing
            if(UnityEngine.Random.Range(0, 10) <= 4)
            {
                order.Add(null);
            }
            else //add ingredient
            {
                order.Add(ingredients[UnityEngine.Random.Range(0, ingredients.Length)]);
            }
        }

        //check if everything is blank
        bool blank = true;
        foreach(GameObject ingredient in order)
        {
            if(ingredient != null)
            {
                blank = false;
                break;
            }
        }

        //do not pass blank orders
        if(blank)
        {
            GenerateOrder(spawnPoint);
        }
        return order;
    }

    //called when customer is spawned, creates orders visually
    public void DisplayOrder(int spawn, List<GameObject> order)
    {
        int i = 0;
        switch(spawn)
        {
            case 1:
            orderBubble1.SetActive(true);
            foreach(GameObject ingredient in order)
            {
                orderIngredients1.Add(Instantiate(ingredient, ingBubblePoints1[i], Quaternion.identity));
                i++;
            }
            break;

            case 2:
            orderBubble2.SetActive(true);
            foreach(GameObject ingredient in order)
            {
                orderIngredients2.Add(Instantiate(ingredient, ingBubblePoints2[i], Quaternion.identity));
                i++;
            }
            break;

            case 3:
            orderBubble3.SetActive(true);
            foreach(GameObject ingredient in order)
            {
                orderIngredients3.Add(Instantiate(ingredient, ingBubblePoints3[i], Quaternion.identity));
                i++;
            }
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