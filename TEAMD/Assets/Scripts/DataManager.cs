using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ingredient
{
    public string id;
    public string name;
}

[System.Serializable]
public class IngredientList //wrapper class
{
    public List<Ingredient> ingredients;
}

[System.Serializable]
public class Recipe
{
    public string id;
    public string ingredients;
    
    public List<string> ParseIngredients()
    {
        return new List<string>(ingredients.Split('#'));
    }
}

[System.Serializable]
public class RecipeList //wrapper class
{
    public List<Recipe> recipes;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [Header("Ingredients")]
    public Dictionary<string, Ingredient> ingredientData = new Dictionary<string, Ingredient>();
    public Dictionary<string, GameObject> ingredientPrefabs = new Dictionary<string, GameObject>();
    public List<GameObject> listofIngrePrefabs = new List<GameObject>(); //assign in inspector

    [Header("Recipes")]
    public Dictionary<string, Recipe> recipeData = new Dictionary<string, Recipe>();


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadIngredientData();
            LoadRecipeData();

            //populate ingredient dictionary
            foreach(GameObject prefab in listofIngrePrefabs)
            {
                ingredientPrefabs[prefab.name] = prefab;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadIngredientData()
    {
        string jsonPath = Path.Combine(Application.streamingAssetsPath, "ingredient.json");

        //if file can't be found
        if(!File.Exists(jsonPath)) { Debug.Log(jsonPath + " cannot be found"); return; }

        string dataString = File.ReadAllText(jsonPath);
        dataString = "{\"ingredients\":" + dataString + "}";

        IngredientList ingredientList = JsonUtility.FromJson<IngredientList>(dataString);
        
        foreach(Ingredient ing in ingredientList.ingredients)
        {
            ingredientData[ing.id] = ing;
        }
        
    }

    void LoadRecipeData()
    {
        string jsonPath = Path.Combine(Application.streamingAssetsPath, "recipe.json");

        //if file can't be found
        if(!File.Exists(jsonPath)) { Debug.Log(jsonPath + " cannot be found"); return; }

        string dataString = File.ReadAllText(jsonPath);
        dataString = "{\"recipes\":" + dataString + "}";

        RecipeList recipeList = JsonUtility.FromJson<RecipeList>(dataString);
        
        foreach(Recipe recipe in recipeList.recipes)
        {
            recipeData[recipe.id] = recipe;
        }
    }

    public string GetIngredientIDByName(string ingredientName)
    {
        foreach(var entry in ingredientData)
        {
            if(entry.Value.name == ingredientName)
            {
                return entry.Key;
            }
        }
        return null;
    }
}

