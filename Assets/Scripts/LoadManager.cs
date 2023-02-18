using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to start loading the next scene
public class LoadManager : MonoBehaviour
{
    LoadingScreen loading;

    void Start()
    {
        loading = GetComponent<LoadingScreen>();
        loading.LoadLevel();
    }
}
