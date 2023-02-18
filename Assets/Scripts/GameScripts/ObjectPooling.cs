using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class instantiate all objects before the game starts to be pooled instead of instantiate in game
public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefabPooling;
    [SerializeField] private int numObjs;
    [SerializeField] private Transform spawnLocation;
    
    private List<GameObject> pooling = new List<GameObject>();
    // Start is called before the first frame update

    private void Awake()
    {
        CreatePooling();
    }

    private void CreatePooling()
    {
        for (int i = 0; i < numObjs; i++)
        {
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        GameObject objClone = Instantiate(prefabPooling, spawnLocation);
        objClone.SetActive(false);
        pooling.Add(objClone);
    }

    public GameObject GetPooledObj()
    {
        GameObject gameObject = null;
        foreach(GameObject obj in pooling)
        {
            if (!obj.activeSelf)
            {
                gameObject = obj;
                return gameObject;
            }
        }
        SpawnPrefab();
        foreach (GameObject obj in pooling)
        {
            if (!obj.activeSelf)
            {
                gameObject = obj;
                return gameObject;
            }
        }
        return gameObject;
    }
}
