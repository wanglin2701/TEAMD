using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [Header("Spawn Settings")]
    public GameObject objectPrefab;   
    public int spawnAmount = 20;      // Number of objects to spawn
    public float spawnInterval = 1.5f;  // Time between each spawn
    
    private Vector2 screenBounds; 

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);  // Wait before spawning the next object
        }
    }

    void Spawn()
    {
        // Generate random spawn position
        Vector2 randomPosition = new Vector2(
            Random.Range(-screenBounds.x + 1f, screenBounds.x - 1f),  // Offset to prevent spawning off-screen
            Random.Range(-screenBounds.y + 1f, screenBounds.y - 1f)
        );

        // Instantiate object at the random position
        Instantiate(objectPrefab, randomPosition, Quaternion.identity);
    }
}
