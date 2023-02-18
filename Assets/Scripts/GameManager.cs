using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to pass information saved between scenes
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private SaveInformation informationSaved;

    public static GameManager Instance { get => instance; }
    public SaveInformation InformationSaved { get => informationSaved; }

    // Start is called before the first frame update
    void Awake()
    {
        SetInstance();

        informationSaved = GetComponent<SaveInformation>();

        DontDestroyOnLoad(this);
    }
    
    private void SetInstance()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
