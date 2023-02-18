using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used on the explosion fx animation
public class TurnOffObject : MonoBehaviour
{
    public void TurnOff()
    {
        this.gameObject.SetActive(false);
    }
}
