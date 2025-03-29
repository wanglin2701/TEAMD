using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public static IngredientSpawner instance;

    [Header("Spawning Logic")]
    public List<Vector3> spawnPoints;
    public float spawnrate = 0.8f; //based off timer in other script, apply multiplier
    float currentPercentage;
    public float easyPercentage = 0.75f; //controls the likelihood of favourable ingredients spawning
    public float normalPercentage = 0.5f;
    public float hardPercentage = 0.25f;

    [Header("Lists of Ingredients")]
    List<string> allIngredients = new List<string>();
    List<string> orderedIngredients = new List<string>();
    List<string> spawnList = new List<string>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentPercentage = easyPercentage;
        
        //add all ingredients to list from data
        foreach(Ingredient ing in DataManager.instance.ingredientData.Values)
        {
            allIngredients.Add(ing.id);
        }
        StartCoroutine(SpawnIngredientRoutine());
    }

    void Update()
    {
        //adjust spawnrate based on time in level in GameManager.cs
        if(GameManager.instance.levelTime <= 120 && GameManager.instance.levelTime > 60)
        {
            spawnrate = 0.5f;
            currentPercentage = normalPercentage;
        }
        else if(GameManager.instance.levelTime <= 60)
        {
            spawnrate = 0.35f;
            currentPercentage = hardPercentage;
        }
    }

    IEnumerator SpawnIngredientRoutine()
    {
        while(true)
        {
            SpawnIngredient();
            yield return new WaitForSeconds(spawnrate);
        }
    }

    // void SpawnIngredient()
    // {   
    //     //randomize a key from the ingredient dictionary
    //     List<string> ingredientKeys = new List<string>(DataManager.instance.ingredientData.Keys);
    //     string randomIngredientKey = ingredientKeys[Random.Range(0, ingredientKeys.Count)];

    //     //select an ingredient using randomized key
    //     Ingredient ingredientToSpawn = DataManager.instance.ingredientData[randomIngredientKey];
    //     Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];

    //     //spawn prefab with same name
    //     if(DataManager.instance.ingredientPrefabs.TryGetValue(ingredientToSpawn.name, out GameObject ingredientPrefab))
    //     {
    //         Instantiate(ingredientPrefab, spawnPosition, Quaternion.identity);
    //     }
    // }

    void SpawnIngredient()
    {
        AdjustIngredientChance(currentPercentage);
    }

    void AdjustIngredientChance(float orderedWeight)
    {
        orderedIngredients = OrderManager.instance.ObserveOrders();

        //randomize whether we're picking from a 'wanted' or 'all' ingredients list
        float spawnChance = Random.Range(0, 1f);
        if(orderedIngredients.Count == 0)
        {
            spawnList = allIngredients;
        }
        else
        {
            spawnList = (spawnChance < orderedWeight)? orderedIngredients : allIngredients;
        }

        //select random key from list
        string randomIngredientKey = spawnList[Random.Range(0, spawnList.Count)];
        
        //select an ingredient using randomized key
        Ingredient ingredientToSpawn = DataManager.instance.ingredientData[randomIngredientKey];
        Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];

        //spawn prefab with same name
        if(DataManager.instance.ingredientPrefabs.TryGetValue(ingredientToSpawn.name, out GameObject ingredientPrefab))
        {
            Instantiate(ingredientPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
