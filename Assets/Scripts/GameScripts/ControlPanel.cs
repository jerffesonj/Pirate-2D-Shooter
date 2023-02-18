using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class controlling the controls panel on start of the game
public class ControlPanel : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TurnOffTutorialPanel());
    }

    IEnumerator TurnOffTutorialPanel()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
