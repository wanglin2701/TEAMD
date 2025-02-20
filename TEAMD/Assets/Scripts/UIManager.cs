using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject levelCompleteScreen;
    public GameObject levelFailScreen;


    public void ShowLevelComplete()
    {
        levelCompleteScreen.SetActive(true);
    }

    public void ShowLevelFail()
    {
        levelFailScreen.SetActive(true);
    }
}
