using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public static IngredientSpawner instance;
    public List<Vector3> spawnPoints;
    public float spawnrate = 0.8f; //based off timer in other script, apply multiplier

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
        InvokeRepeating(nameof(SpawnIngredient), 3f, spawnrate);
    }

    void Update()
    {
        //adjust spawnrate based on time in level in GameManager.cs
        if(GameManager.instance.levelTime <= 120 && GameManager.instance.levelTime > 60)
        {
            spawnrate =- 0.3f;
        }
        else if(GameManager.instance.levelTime <= 60)
        {
            spawnrate =- 0.2f;
        }
    }

    void SpawnIngredient()
    {
        //randomize a key from the ingredient dictionary
        List<string> ingredientKeys = new List<string>(DataManager.instance.ingredientData.Keys);
        string randomIngredientKey = ingredientKeys[Random.Range(0, ingredientKeys.Count)];

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
