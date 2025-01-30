using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public List<GameObject> ingredients;
    public List<Vector3> spawnPoints;
    public float spawnrate = 0.5f; //based off timer in other script, apply multiplier


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnIngredient), 3f, spawnrate);
    }

    void SpawnIngredient()
    {
        GameObject ingredientToSpawn = ingredients[Random.Range(0, ingredients.Count)];
        Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];

        Instantiate(ingredientToSpawn, spawnPosition, Quaternion.identity);
    }
}
