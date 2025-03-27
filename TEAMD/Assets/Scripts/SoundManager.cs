using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundManagerSource;
    public AudioSource musicManagerSource;
    private AudioSource[] audioSources;

    public AudioClip BTNClick;
    public AudioClip BTNHover;
    public AudioClip ClearPlate;
    public AudioClip CorrectOrder;
    public AudioClip CustomerSpawn;
    public AudioClip GameoverSound;
    public AudioClip HoldingOnIngredient;  //While click and dragging ingredients
    public AudioClip IngredientPlate;  //Ingredient enter plate
    public AudioClip LoadingSound;
    public AudioClip WrongOrder;
    public AudioClip CustomerLeft;
    public AudioClip SmallAlien;  //When Spawn
    public AudioClip RedAlien;   //When Spawn
    public AudioClip BlueAlien;    //When Spawn
    public AudioClip PickUpPlate;
    public AudioClip PlateFull;
    public AudioClip NotEnoughSpeed;  //When Ingredient not enuff velocity
    public AudioClip LevelWin;
    public AudioClip BigBlueAlien;
    public AudioClip ChangeOrder;


    public AudioClip BGMusic;
    public AudioClip GameplayMusic;


    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSources = GetComponentsInChildren<AudioSource>();
            soundManagerSource = audioSources[0];
            musicManagerSource = audioSources[1];

        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(string name)
    {
        switch (name)
        {
            case "BTNClick":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(BTNClick);
                break;

            case "BTNHover":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(BTNHover);
                break;

            case "ClearPlate":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(ClearPlate);
                break;

            case "Correct":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(CorrectOrder);
                break;

            case "CustomerSpawn":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(CustomerSpawn);
                break;

            case "HoldIngredient":  
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(HoldingOnIngredient);
                break;

            case "IngredientEnter":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(IngredientPlate);
                break;

            case "Loading":    //Not Added
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(LoadingSound);
                break;

            case "Wrong":  
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(WrongOrder);
                break;

            case "CustomerLeft": 
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(CustomerLeft);
                break;

            case "SmallAlien": 
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(SmallAlien);
                break;

            case "RedAlien":  
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(RedAlien);
                break;

            case "BlueAlien":   
                soundManagerSource.volume = 0.25f;

                soundManagerSource.PlayOneShot(BlueAlien);
                break;

            case "PickUpPlate":
                soundManagerSource.volume = 1f;

                soundManagerSource.PlayOneShot(PickUpPlate);
                break;

            case "Gameover":
                soundManagerSource.volume = 1.5f;

                soundManagerSource.PlayOneShot(GameoverSound);
                break;

            case "PlateFull":  
                soundManagerSource.volume = 1f;

                soundManagerSource.PlayOneShot(PlateFull);
                break;

            case "LevelWin":   
                soundManagerSource.volume = 1f;

                soundManagerSource.PlayOneShot(LevelWin);
                break;

            case "BigBlueAlien":
                soundManagerSource.volume = 0.90f;

                soundManagerSource.PlayOneShot(BigBlueAlien);
                break;

            case "ChangeOrder":
                soundManagerSource.volume = 0.90f;

                soundManagerSource.PlayOneShot(ChangeOrder);
                break;
        }
    }

    public void PlayMusic()
    {
        musicManagerSource.loop = true;
        musicManagerSource.Play();
    }

    public void ChangeMusic(string name)
    {

        switch (name)
        {
            case "BG":
                Debug.Log(musicManagerSource);

                if (musicManagerSource.clip != BGMusic)
                {
                    musicManagerSource.clip = BGMusic;
                    PlayMusic();
                }
           
                break;

            case "Gameplay":

                if (musicManagerSource.clip != GameplayMusic)
                {
                    musicManagerSource.clip = GameplayMusic;
                    PlayMusic();
                }
                break;


        }
    }

}
