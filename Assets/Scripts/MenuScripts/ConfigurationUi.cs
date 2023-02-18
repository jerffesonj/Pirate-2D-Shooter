using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfigurationUi : MonoBehaviour
{
    private int gameTime;
    private float spawnTime;
    public TMP_InputField gameTimeInput;
    public TMP_InputField spawnTimeInput;

    public delegate void SaveGameTime(int gameTime);
    public delegate void SaveSpawnTime(float SpawnTime);
    public static event SaveGameTime OnSaveGameTime;
    public static event SaveSpawnTime OnSaveSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        SaveInformation.onLoadValues += StartInputValues;

    }
    private void OnDisable()
    {
        SaveInformation.onLoadValues -= StartInputValues;

    }

    void StartInputValues(int gameTime, float spawnTime)
    {
        //gameTimeInput.placeholder.GetComponent<TMP_Text>().text = gameTime.ToString();
        //spawnTimeInput.placeholder.GetComponent<TMP_Text>().text = spawnTime.ToString();
    }

    public void ChangeGameTimeInfo()
    {
        int.TryParse(gameTimeInput.text, out int gameTimeInt);
        if (gameTimeInt >= 60)
        {
            if (gameTimeInt < 180)
            {
                gameTime = gameTimeInt;
            }
            else
            {
                gameTime = 180;
            }
        }
        else
            gameTime = 60;
        OnSaveGameTime?.Invoke(gameTime);
    }
    public void ChangeSpawnTimeInfo()
    {
        float.TryParse(spawnTimeInput.text, out float spawnTimeInt);
        if (spawnTimeInt > 0.5f)
            spawnTime = spawnTimeInt;
        else
            spawnTime = 0.5f;
        OnSaveSpawnTime?.Invoke(spawnTime);
    }

}
