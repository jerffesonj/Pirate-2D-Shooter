using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible to Save and Load the game time and spawn time information
public class SaveInformation : MonoBehaviour
{
    public int gameTime;
    public float spawnTime;

    //Delegate to call the method when it loads
    public delegate void LoadValues(int gameTime, float SpawnTime);
    public static event LoadValues onLoadValues;

    void Awake()
    {
        Load();
    }
    private void OnEnable()
    {
        ConfigurationUi.OnSaveGameTime += SetGameTime;
        ConfigurationUi.OnSaveSpawnTime += SetSpawnTime;
    }
    private void OnDisable()
    {
        ConfigurationUi.OnSaveGameTime -= SetGameTime;
        ConfigurationUi.OnSaveSpawnTime -= SetSpawnTime;
    }

    public void SetGameTime(int value)
    {
        gameTime = value;
    }

    public void SetSpawnTime(float value)
    {
        spawnTime = value;
    }

    //Save method
    public void Save()
    {
        PlayerPrefs.SetInt("GameTime", gameTime);
        PlayerPrefs.SetFloat("SpawnTime", spawnTime);
    }

    //Load method
    public void Load()
    {
        gameTime = PlayerPrefs.GetInt("GameTime");
        if (gameTime == 0)
        {
            gameTime = 60;
        }
        spawnTime = PlayerPrefs.GetFloat("SpawnTime");
        if (spawnTime == 0)
        {
            spawnTime = 2;
        }
        onLoadValues?.Invoke(gameTime, spawnTime);
    }
}
